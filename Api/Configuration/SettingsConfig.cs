using Application;

namespace API
{
    public static class SettingsConfig
    {
        public static void AddSettings(this IServiceCollection services, ConfigurationManager config)
        {
            var settings = config.Get<SettingsDto>();

            services.AddSingleton(settings);
        }
    }
}
