using CurrencyCalculatorSystem.Application.Contracts.ContextApplication;
using System.Security.Claims;

namespace CurrencyCalculatorSystem.Presentation.Middlewares
{
    public class HttpCurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
    {
        private readonly string _userIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private readonly Guid _systemUserId = new("00000000-0000-0000-0000-000000000001");

        public Guid UserId => GetGuidClaim(_userIdClaimType) ?? _systemUserId;

        public string Name => httpContextAccessor.HttpContext?.User.FindFirstValue("name") ?? "System";
        public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue("preferred_username") ?? "system@actinver.com.mx";
        public string Department => httpContextAccessor.HttpContext?.User.FindFirstValue("department") ?? "System";
        public string OfficeLocation => httpContextAccessor.HttpContext?.User.FindFirstValue("officeLocation") ?? "System";
        public string? GetToken()
        {
            var token = httpContextAccessor.HttpContext?.Request.Headers.Authorization
                .FirstOrDefault()?.Split(" ").Last();
            return token;
        }

        private Guid? GetGuidClaim(string type)
        {
            var value = httpContextAccessor.HttpContext?.User.FindFirstValue(type);
            return Guid.TryParse(value, out var guid) ? guid : null;
        }
    }
}
