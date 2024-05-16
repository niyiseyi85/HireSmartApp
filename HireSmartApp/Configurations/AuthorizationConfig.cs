﻿using HireSmartApp.API.Configurations.Requirement;
using HireSmartApp.API.Security;
using HireSmartApp.API.Security.Authorization;
using HireSmartApp.Core.Security;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;

namespace HireSmartApp.API.Configurations
{   
    public static class AuthorizationConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new PasswordPolicy
            {
                MinimumLength = int.Parse(configuration["PasswordPolicy:MinimumLength"]),
                MaximumLength = int.Parse(configuration["PasswordPolicy:MaximumLength"]),
                SymbolsRequired = bool.Parse(configuration["PasswordPolicy:Symbol"]),
                NumericCharactersRequired = bool.Parse(configuration["PasswordPolicy:Numeric"]),
                LowercaseCharactersRequired = bool.Parse(configuration["PasswordPolicy:Lowercase"]),
                UppercaseCharactersRequired = bool.Parse(configuration["PasswordPolicy:Uppercase"])
            });

            services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, RequiresClaimHandler>();
            services.AddTransient<IIdentityUserContext, HttpIdentityUserContext>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPasswordService, PasswordService>();


            services.AddAuthorization();

            services.AddSingleton<IAuthorizationPolicyProvider, RequiresClaimsPolicyProvider>();
        }
    }
}
