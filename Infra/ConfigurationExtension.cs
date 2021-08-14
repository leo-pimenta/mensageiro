namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static string[] GetArray(this IConfiguration configuration, string path)
            => configuration[path].Split(",");
    }
}