using Microsoft.Extensions.Configuration;
using ToDoApi.Application.Interfaces.Helpers;

namespace ToDoApi.Infrastructure.Helpers
{
    public class ConfigurationHelper: IConfigurationHelper
    {
        private readonly IConfiguration _cofiguration;
        public ConfigurationHelper(IConfiguration configuration)
        {
            _cofiguration = configuration;
        }

        public string GetJwtValidAudience(string key)
        {
            return _cofiguration[key];
        }

        public string GetJwtValidIssuer(string key)
        {
            return _cofiguration[key];
        }
        public string GetJwtSecret(string key)
        {
            return _cofiguration[key];
        }
        public int GetJwtTtl(string key)
        {
            return int.Parse(_cofiguration[key]);
        }
    }
}
