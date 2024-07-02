using EmailToolkit.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailToolkit
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // add options
            services.AddOptions();
            services.Configure<ApplicationOptions>(Configuration.GetSection("Settings"));

            // add app and services
            services.AddTransient<App>();
            services.AddTransient<IGoogleService, GoogleService>();
            services.AddTransient<IGoogleClient, GoogleClient>();
        }
    }
}