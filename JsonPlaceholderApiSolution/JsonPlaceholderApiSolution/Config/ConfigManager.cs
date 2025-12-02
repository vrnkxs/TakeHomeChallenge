using Microsoft.Extensions.Configuration;

namespace ApiTests.Config
{
    /*
     * Configuration loader for API tests.
     * Loads configuration values from appsettings.json.
     */
    public class ConfigManager
    {
        private static readonly IConfigurationRoot _config;

        /*
         * Static contractor executes once when the class is first used.
         * Loads appsettings.json into the IConfiguration object.
         */
        static ConfigManager()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }

        /*
         * Exposes the BaseUrl value from appsettings.json.
         * Ensures API tests stay environment-independent.
         */
        public static string BaseUrl => _config["baseUrl"];
    }
}