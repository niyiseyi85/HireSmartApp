using HireSmartApp.Core.Models.Mappings;
using System.Reflection;

namespace HireSmartApp.API.Configurations
{    
    public static class AutoMapperConfig
    {
        public static void Configure(IServiceCollection services, params Assembly[] additionalAssemblies)
        {
            services.AddAutoMapper(typeof(MappingConfig).Assembly);
            services.AddAutoMapper(additionalAssemblies);
        }
    }
}
