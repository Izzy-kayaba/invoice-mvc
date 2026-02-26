using System.Security.Claims;

namespace InvoiceGenerator.Api.Extensions
{
    /// <summary>
    /// Helper methods for extracting data from JWT claims.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException("UserId claim missing");

            return Guid.Parse(claim.Value);
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new UnauthorizedAccessException("Role claim missing");
        }
    }
}