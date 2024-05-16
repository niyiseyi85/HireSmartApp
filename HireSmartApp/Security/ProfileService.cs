using HireSmartApp.Core.Security;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace HireSmartApp.API.Security
{
    public static class HireSmartClaimTypes
    {
        public static string FirstName { get { return "FirstName"; } }
        public static string LastName { get { return "LastName"; } }
        public static string RoleId { get { return "RoleId"; } }
        public static string RoleName { get { return "RoleName"; } }
        public static string UserId { get { return "UserId"; } }
        public static string HasWebPortalAccess { get { return "HasWebPortalAccess"; } }
    }

    public class ProfileService : IProfileService
    {
        private readonly ILoginService _loginService;
        // readonly ILogger _logger;

        public ProfileService(ILoginService loginService)
        {
            _loginService = loginService;
            //_logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var loginId = GetLoginId(context.Subject.GetSubjectId());
            var login = loginId.HasValue ? await _loginService.GetLoginById(loginId.Value) : null;

            if (login != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, login.Id.ToString()),
                    new Claim(IdentityClaims.UserClaim, login.Username ?? string.Empty),
                    new Claim(HireSmartClaimTypes.FirstName, login.FirstName?? string.Empty),
                    new Claim(HireSmartClaimTypes.LastName, login.LastName ?? string.Empty),
                    new Claim(HireSmartClaimTypes.RoleId, login.RoleId.ToString() ?? string.Empty),
                    new Claim(HireSmartClaimTypes.RoleName, login.Username ?? string.Empty),
                    new Claim(HireSmartClaimTypes.UserId, login.Id.ToString() ?? string.Empty),
            };
                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var loginId = GetLoginId(context.Subject.GetSubjectId());
            var login = loginId.HasValue ? await _loginService.GetLoginById(loginId.Value) : null;

            context.IsActive = login != null;
        }

        private int? GetLoginId(string subjectId)
        {
            if (int.TryParse(subjectId, out var loginId))
                return loginId;

            return null;
        }
    }
}
