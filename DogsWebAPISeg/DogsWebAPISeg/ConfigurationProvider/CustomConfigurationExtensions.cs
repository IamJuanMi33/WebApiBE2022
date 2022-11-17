namespace DogsWebAPISeg.CustomConfigurationProvider
{
    public static class CustomConfigurationExtensions
    {
        public static IConfigurationBuilder AddSecurityConfiguration
        (this IConfigurationBuilder builder)
        {
            return builder.Add(new CustomConfigurationSource());
        }
    }
}
