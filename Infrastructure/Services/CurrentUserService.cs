using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // This method retrieves the value of a specific claim type from the current user's claims.
        // If the claim is not found or the user is not authenticated, it throws an UnauthorizedAccessException.
        public string GetClaimValue(string claimType)
        {
            var claimValue = _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
            if (string.IsNullOrEmpty(claimValue))
            {
                throw new UnauthorizedAccessException($"Claim '{claimType}' is missing or user is not authenticated.");
            }
            return claimValue;
        }

        public string UserId => GetClaimValue(ClaimTypes.NameIdentifier);

        public string Role => GetClaimValue(ClaimTypes.Role);
    }

    /// Extension method to simplify the retrieval of claim values
    public static class ClaimsPrincipalExtensions
    {
        public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            return principal?.FindFirst(claimType)?.Value;
        }
    }

}