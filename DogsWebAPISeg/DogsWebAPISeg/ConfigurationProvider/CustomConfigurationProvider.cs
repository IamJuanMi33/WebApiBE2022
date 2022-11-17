using System.Text.Json;

namespace DogsWebAPISeg.CustomConfigurationProvider
{
    public class CustomConfigurationProvider : 
        Microsoft.Extensions.Configuration.ConfigurationProvider
    {
        public override void Load()
        {
            var text = File.ReadAllText(@"D:\SecurityMetadata.json");
            var options = new JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var content = JsonSerializer.Deserialize<SecurityMetadata>
           (text, options);
            if (content != null)
            {
                Data = new Dictionary<string, string>
                {
                    {"ApiKey", content.ApiKey},
                    {"ApiSecret", content.ApiSecret}
                };
            }
        }
    }
}
