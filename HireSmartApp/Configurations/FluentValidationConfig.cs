using FluentValidation;
using Microsoft.Extensions.Options;

namespace HireSmartApp.API.Configurations
{  
    public static class FluentValidationConfig
    {
        public static void Configure()
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        }
    }
}
