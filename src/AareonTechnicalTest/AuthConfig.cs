using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest
{
    public class AuthConfig
    {
        public const string Name = nameof(AuthConfig);

        [Required]
        public string Secret { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        public int AccessTokenExpiration { get; set; } = 10;
    }
}