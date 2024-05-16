using HireSmartApp.Core.Data.Repository;
using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Sevices;
using HireSmartApp.Core.Sevices.IService;

namespace HireSmartApp.API.Configurations
{    
    public static class ServiceExtensionConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddScoped(typeof(GenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IProgramService, ProgramService>();

            //services.AddScoped<IValidator<AddClaimDto>, AddClaimDtoValidator>();
            //services.AddScoped<IValidator<AddUserDto>, AddUserDtoValidator>();
            //services.AddScoped<IValidator<ReviewClaimDto>, ReviewClaimDtoValidator>();
        }
    }
}
