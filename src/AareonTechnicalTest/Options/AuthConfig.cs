using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Options
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
