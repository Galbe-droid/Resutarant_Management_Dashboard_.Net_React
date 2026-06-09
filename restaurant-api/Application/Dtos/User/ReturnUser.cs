using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.Application.Dtos.User
{
    public class ReturnUser
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
