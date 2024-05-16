using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HireSmartApp.API.Security.Authorization
{   
    public static class DefaultPolicy
    {
        public static AuthorizationPolicy Build() => new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    }
}
