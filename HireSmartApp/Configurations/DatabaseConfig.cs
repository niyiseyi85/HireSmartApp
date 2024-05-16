using HireSmartApp.Core.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace HireSmartApp.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<DataContext>();

            var endpointUri = config["AzureCosmos:EndpointUri"];
            var primaryKey = config["AzureCosmos:PrimaryKey"];
            var database = config["AzureCosmos:Database"];

            //services.AddSingleton((provider) =>
            //{
            //    var cosmosClientOPtion = new CosmosClientOptions
            //    {
            //        ApplicationName = database
            //    };
            //    var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOPtion);

            //    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;

            //    return cosmosClient;
            //});


            
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseCosmos(endpointUri, primaryKey, database);
                });
            

        }
    }
}
