using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Entities.UserRelated;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwt;
        private readonly PasswordHasher<User> _hasher;
        // Change the type of _dBContext to match your actual DbContext, e.g., MyDbContext
        private readonly RestaurantDbContext _dBContext;
        public AuthService(RestaurantDbContext dBContext, JwtService jwt)
        {
            _jwt = jwt;
            _hasher = new PasswordHasher<User>();
            _dBContext = dBContext;
        }
        public async Task<Result<bool>> DeleteUserAsync(Guid userId)
        {
            _dBContext.CurrentUserId = userId;
            var user = await _dBContext.Users.FindAsync(userId);

            if (user == null)
            {
                return Result<bool>.Fail("User Not Found");
            }

            user.IsDeleted = true;

            await _dBContext.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<ReturnUser>> GetUserAsync(string username)
        {
            var user = await _dBContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return Result<ReturnUser>.Fail("User Not Found");
            }

            return Result<ReturnUser>.Ok(UserMapping.ToReturnUser(user));
        }

        public async Task<Result<AuthTokenResponse>> LoginAsync(LoginUser login)
        {
            User? user = null;

            if (login.Login.Contains('@') && user == null)
            {
                user = await _dBContext.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Email == login.Login);
            }
            else
            {
                 user = await _dBContext.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Username == login.Login);
            }

            if (user == null)
            {
                return Result<AuthTokenResponse>.Fail("User Not Found");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Result<AuthTokenResponse>.Fail("Incorrect Password");
            }

            Console.WriteLine(user.Email);
            Console.WriteLine(user.PasswordHash);
            Console.WriteLine(user.UserRoles.Count);

            var token = _jwt.GenerateToken(user);

            var activeTokens = await _dBContext.RefreshTokens.Where(r => r.UserId == user.Id && !r.IsRevoked).ToListAsync();

            foreach (var tok in activeTokens)
            {
                tok.IsRevoked = true;
            }

            var refresh = new RefreshToken
            {
                Token = _jwt.GenerateRefreshToken(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };           

            _dBContext.RefreshTokens.Add(refresh);

            await RemoveExpiredTokens();

            await _dBContext.SaveChangesAsync();

            Console.WriteLine("Cheguei aqui 1: " + login.Login + ", " + login.Password);

            return Result<AuthTokenResponse>.Ok(new AuthTokenResponse
            {
                AccessToken = token,
                RefreshToken = refresh.Token
            });
        }
        public async Task<Result<AuthTokenResponse>> RefreshAsync(TokenRequest request, Guid userId)
        {
            _dBContext.CurrentUserId = userId;
            var stored = await _dBContext.RefreshTokens
                .Include(x => x.User)
                .ThenInclude(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (stored == null || stored.IsRevoked || stored.ExpiresAt < DateTime.UtcNow)
                return Result<AuthTokenResponse>.Fail("Unauthorized");

            stored.IsRevoked = true;

            var newRefresh = new RefreshToken
            {
                Token = _jwt.GenerateRefreshToken(),
                UserId = stored.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _dBContext.RefreshTokens.Add(newRefresh);

            var token = _jwt.GenerateToken(stored.User);

            await RemoveExpiredTokens();

            await _dBContext.SaveChangesAsync();

            return Result<AuthTokenResponse>.Ok(new AuthTokenResponse
            {
                AccessToken = token,
                RefreshToken = newRefresh.Token
            });
        }
        public async Task<Result<bool>> LogoutAsync(TokenRequest request, Guid userId)
        {
            _dBContext.CurrentUserId = userId;
            var token = await _dBContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (token != null)
            {
                token.IsRevoked = true;
                await _dBContext.SaveChangesAsync();
            }

            return Result<bool>.Ok(true);
        }
        public async Task<(bool Sucess, object? Errors)> RegisterAdminAsync(RegisterUser register)
        {
            var errors = new Dictionary<string, string>();

            if (await _dBContext.Users.AnyAsync(x => x.Username == register.Username))
                errors["username"] = "Username already exists";

            if (await _dBContext.Users.AnyAsync(x => x.Email == register.Email))
                errors["email"] = "Email already exists";

            if (errors.Any())
                return (false, errors);

            var user = UserMapping.ToUser(register);

            Role userRole = await _dBContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            user.UserRoles.Add(new UserRole { RoleId = userRole!.Id, Role = userRole, UserId = user.Id, User = user });

            user.PasswordHash = _hasher.HashPassword(user, register.Password);

            await _dBContext.Users.AddAsync(user);

            await _dBContext.SaveChangesAsync();

            return (true, null);
        }

        private async Task RemoveExpiredTokens()
        {
            var expiredTokens = await _dBContext.RefreshTokens
                .Where(r =>
                    r.IsRevoked &&
                    r.ExpiresAt < DateTime.UtcNow)
                .ToListAsync();

            _dBContext.RefreshTokens.RemoveRange(expiredTokens);

            await _dBContext.SaveChangesAsync();
        }
    }
}
