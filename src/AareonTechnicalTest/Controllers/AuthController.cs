using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AareonTechnicalTest.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext dbContext;
        private readonly IOptions<AuthConfig> authConfig;

        public AuthController(ApplicationContext dbContext, IOptions<AuthConfig> authConfig)
        {
            this.dbContext = dbContext;
            this.authConfig = authConfig;
        }


        // GET: api/<TicketController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] int personId)
        {
            var person = await dbContext.Persons.FindAsync(personId);

            if (person is null)
            {
                return NotFound();
            }

            var claimsList = new List<Claim>
            {
                new (ClaimTypes.Sid, person.Id.ToString()),
                new (ClaimTypes.Name, $"{person.Forename} {person.Surname}"),
            };

            if (person.IsAdmin)
            {
                claimsList.Add(new(ClaimTypes.Role, "Admin"));
            }

            var jwtToken = new JwtSecurityToken(
                this.authConfig.Value.Issuer,
                this.authConfig.Value.Audience,
                claimsList,
                expires: DateTime.UtcNow.AddMinutes(this.authConfig.Value.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.authConfig.Value.Secret)), SecurityAlgorithms.HmacSha256Signature));
            return this.Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}
