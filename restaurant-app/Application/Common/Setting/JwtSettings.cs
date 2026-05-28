namespace Template_restaurant_app.Application.Common.Setting
{
    // Class to hold JWT settings loaded from configuration
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
