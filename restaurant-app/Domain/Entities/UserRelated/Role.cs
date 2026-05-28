using System.Collections;

namespace Template_restaurant_app.Domain.Entities.UserRelated
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
