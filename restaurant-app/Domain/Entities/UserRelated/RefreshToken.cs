namespace Template_restaurant_app.Domain.Entities.UserRelated
{
    // Refrash token entity representing a token used for refreshing JWT tokens
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString();

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
