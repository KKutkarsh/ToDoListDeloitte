namespace ToDoApi.Application.Interfaces.Helpers
{
    public interface IConfigurationHelper
    {
        string GetJwtValidIssuer(string key);
        string GetJwtValidAudience(string key);
        public string GetJwtSecret(string key);
        public int GetJwtTtl(string key);
    }
}
