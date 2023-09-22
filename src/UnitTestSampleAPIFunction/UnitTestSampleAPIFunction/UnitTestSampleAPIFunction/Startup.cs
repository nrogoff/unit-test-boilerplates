using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleDAL;
using SampleRepository.Repositories;


[assembly: FunctionsStartup(typeof(UnitTestSampleAPIFunction.Startup))]
namespace UnitTestSampleAPIFunction
{
    /// <summary>
    /// This class is used to configure the dependency injection container for the Azure Function.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            // No need to register the TelemetryClient as it is already registered by the Microsoft.NET.Sdk.Functions package
            // No need to register the IConfiguration as it is already registered by the Microsoft.NET.Sdk.Functions package
            // Applications Insights is already configured by the Microsoft.NET.Sdk.Functions package

            // Get the configuration from the Azure Function
            IConfiguration configuration = builder.Services.BuildServiceProvider()
                .GetService<IConfiguration>();

            var title = configuration["MyOptions:OpenApiTitle"];

            // Add the 'MyOptions' configurations to a custom type. Copying the app settings values to a custom type makes it easier to test your services by making these values injectable.
            // Calling Bind copies values that have matching property names from the configuration into the custom instance. 
            //builder.Services.AddOptions<MyOptions>()
            //    .Configure<IConfiguration>(
            //    (settings, config) =>
            //    {
            //        _config.GetSection("MyOptions").Bind(settings);
            //    });

            ////_config.GetSection("MyOptions").Bind(builder.Services);

            // Add the Application Insights Telemetry to the collection of services
            // NOTE: This is not required if you are using the Microsoft.NET.Sdk.Functions package
            // builder.Services.AddApplicationInsightsTelemetry();

            // Add the HttpClient to the collection of services. This is used to consume other APIs.
            //builder.Services.AddHttpClient();

            // Add AutoMapper to the collection of services
            builder.Services.AddAutoMapper(typeof(Startup));

            // Add the dbContext to the collection of services
            // The 'AddDbContext' method ensures that only one instance of the DbContext is created and shared throughout the lifetime of the application, improving performance and reducing the likelihood of concurrency issues.
            builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")));

            // Add Repositories to the collection of services
            // The 'AddScoped' method registers a service with the container and specifies that a new instance of the service should be created once per client request (i.e. scoped lifetime).
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            
        }
    }

    /// <summary>
    /// This class is used to store the 'MyOptions' configuration values.
    /// </summary>
    public class MyOptions
    {
        public string OpenApiTitle { get; set; }
    }
}