using Microsoft.AspNetCore.Mvc;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.User;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Sucess, object? Errors)> RegisterAdminAsync(RegisterUser register);
        Task<Result<AuthTokenResponse>> LoginAsync(LoginUser login);
        Task<Result<AuthTokenResponse>> RefreshAsync(TokenRequest request, Guid userId);
        Task<Result<bool>> LogoutAsync(TokenRequest request, Guid userId);
        Task<Result<ReturnUser>> GetUserAsync(string username);
        Task<Result<bool>> DeleteUserAsync(Guid userId);
    }
}
