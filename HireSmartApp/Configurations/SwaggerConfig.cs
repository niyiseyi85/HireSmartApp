using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HireSmartApp.API.Configurations
{
    public static class SwaggerConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            if ($"{config["Environment"]}" == "Development")
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Hire Smart API",
                        Version = "v1",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Hire Smart System"
                        },
                        Description = "<h2>Description</h2><p>This is the API for the Hiring candidate.</p><br /><h3>Usage</h3><ul><li><strong>Authorization (header)</strong><ul><li></li></ul></li></ul>"
                    });

                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Password = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{config["Identity:Authority"]}connect/authorize"),
                                TokenUrl = new Uri($"{config["Identity:Authority"]}connect/token")
                            }
                        }
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            new[] { IdentityServerConfig.ApiResourceName }
                        }
                    });

                    //options.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}/Swagger.XML");
                });

            }
        }

        public static void Configure(IApplicationBuilder app, IConfiguration config)
        {
            if ($"{config["Environment"]}" == "Development")
            {
                var clientId = config
                            .GetSection("Identity:Clients")
                            .Get<IEnumerable<Client>>()
                            .Select(x => x.ClientId)
                            .First();

                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    //options.RoutePrefix = "docs";
                    options.DocExpansion(DocExpansion.None);
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Health Insurance API v1");

                    options.OAuthClientId(clientId);
                    options.OAuthClientSecret(config["Identity:ClientSecret"]);
                });
            }
        }
    }
}
