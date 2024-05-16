using CSharpFunctionalExtensions;
using HireSmartApp.Core.Security;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace HireSmartApp.API.Security
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public const string Grant = "password";
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;

        public ResourceOwnerPasswordValidator(ILoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var login = await _loginService.GetLoginByEmail(context.UserName, context.Password);

                Result validateResponse = Result.Combine(login);

                if (validateResponse.IsFailure)
                {
                    if (validateResponse.Error.ToLower() == "Confirm your email before accessing Hire Smart.".ToLower())
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, $"{validateResponse.Error}");
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"{validateResponse.Error}");
                    }
                }
                else
                {
                    context.Result = new GrantValidationResult(login.Value.Id.ToString(), Grant);
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest,
                           $"Error Details: {ex.Message}");
            }
        }
    }
}
