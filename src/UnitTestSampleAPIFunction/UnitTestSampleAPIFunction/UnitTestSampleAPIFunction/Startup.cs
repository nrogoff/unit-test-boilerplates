//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;

//[assembly: FunctionsStartup(typeof(UnitTestSampleAPIFunction.Startup))]
//namespace UnitTestSampleAPIFunction
//{
//    /// <summary>
//    /// This class is used to configure the dependency injection container for the Azure Function.
//    /// </summary>
//    public class Startup : FunctionsStartup
//    {
//        public override void Configure(IFunctionsHostBuilder builder)
//        {

//            // No need to register the TelemetryClient as it is already registered by the Microsoft.NET.Sdk.Functions package
//            // No need to register the IConfiguration as it is already registered by the Microsoft.NET.Sdk.Functions package
//            // Applications Insights is already configured by the Microsoft.NET.Sdk.Functions package

//            // Get the configuration from the Azure Function
//            //IConfiguration _config = builder.GetContext().Configuration;

//            var title = System.Environment.GetEnvironmentVariable("MyOptions:OpenApiTitle");

//            // Add the 'MyOptions' configurations to a custom type. Copying the app settings values to a custom type makes it easier to test your services by making these values injectable.
//            // Calling Bind copies values that have matching property names from the configuration into the custom instance. 
//            //builder.Services.AddOptions<MyOptions>()
//            //    .Configure<IConfiguration>(
//            //    (settings, config) =>
//            //    {
//            //        _config.GetSection("MyOptions").Bind(settings);
//            //    });

//            ////_config.GetSection("MyOptions").Bind(builder.Services);

//            // Add the Application Insights Telemetry to the collection of services
//            // NOTE: This is not required if you are using the Microsoft.NET.Sdk.Functions package
//            // builder.Services.AddApplicationInsightsTelemetry();

//            // Add the HttpClient to the collection of services. This is used to consume other APIs.
//            //builder.Services.AddHttpClient();

//            // break code execution here to debug the startup class
//            //Debugger.Break();

//        }
//    }

//    /// <summary>
//    /// This class is used to store the 'MyOptions' configuration values.
//    /// </summary>
//    public class MyOptions
//    {
//        public string OpenApiTitle { get; set; }
//    }
//}