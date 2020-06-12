namespace TestWebFast
{
    using Microsoft.Extensions.Configuration;

    public class SettingsManager
    {
        public static IConfigurationRoot Configuration { get; } = SettingsBuilder();

        public static string GetSettingsValue(params string[] args)
        {
            if (args.Length == 1)
            {
                return Configuration[$"{args[0]}"];
            }

            int index = 0;
            string paramsString = string.Empty;

            foreach (var arg in args)
            {
                paramsString = $"{paramsString}{args[index]}:";
                index++;
            }

            return Configuration[paramsString.TrimEnd(':')];
        }

        public static object GetSettingsValues(params string[] args)
        {
            if (args.Length == 1)
            {
                return Configuration[$"{args[0]}"];
            }

            int index = 0;
            string paramsString = string.Empty;

            foreach (var arg in args)
            {
                paramsString = $"{paramsString}{args[index]}:";
                index++;
            }

            return Configuration[paramsString.TrimEnd(':')];
        }

        private static IConfigurationRoot SettingsBuilder()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("action-protocol.json", false, reloadOnChange: true)
                .AddJsonFile("seleniumSettings.json", false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
