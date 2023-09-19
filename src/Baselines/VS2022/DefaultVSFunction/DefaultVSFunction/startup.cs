
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(DefaultVSFunction.Startup))]

namespace DefaultVSFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.Services.BuildServiceProvider()
                .GetService<IConfiguration>();
            string myAppSetting = configuration["MyAppSetting"];
            // use myAppSetting or any other configuration value you need
            // add your services here
        }
    }
}
