namespace Template_restaurant_app.Domain.Entities.UserRelated
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public bool IsDeleted { get; set; } = false;
    }
}
