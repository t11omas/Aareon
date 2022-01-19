using System;
using System.Linq;
using System.Security.Claims;

namespace AareonTechnicalTest
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var uId = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)
                ?.Value;

            return string.IsNullOrEmpty(uId) ? throw new Exception("UserId not found") : int.Parse(uId);
        }
    }
}