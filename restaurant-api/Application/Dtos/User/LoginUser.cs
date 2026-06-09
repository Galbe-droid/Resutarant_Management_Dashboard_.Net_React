using System.ComponentModel.DataAnnotations;

namespace Template_restaurant_app.Application.Dtos.User
{
    public class LoginUser
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
