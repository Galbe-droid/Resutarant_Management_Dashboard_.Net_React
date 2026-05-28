using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Domain.Entities.UserRelated;

namespace Template_restaurant_app.API.Mappers
{
    public class UserMapping
    {
        public static ReturnUser ToReturnUser(User user)
        {
            return new ReturnUser
            {
                Email = user.Email,
                Name = user.Name,
                Username = user.Username,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }

        public static User ToUser(RegisterUser register)
        {
            return new User
            {
                Username = register.Username,
                Name = register.Name,
                Email = register.Email,
            };
        }

        public static User ToUser(RegisterRestaurantUser register) {
            return new User
            {
                Username = register.Username,
                Name = register.Name,
                Email = register.Email,
            };
        }
    }
}
