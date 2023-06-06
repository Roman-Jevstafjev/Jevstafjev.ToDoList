using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jevstafjev.ToDoList.Services
{
    public class AccessTokenService
    {
        private readonly string _configurationFilePath = "AppSettings.json";

        public string GetToken()
        {
            var jsonString = File.ReadAllText(_configurationFilePath);
            dynamic configuration = JObject.Parse(jsonString);

            return configuration.AccessToken;
        }

        public void SetToken(string token)
        {
            var jsonString = File.ReadAllText(_configurationFilePath);

            dynamic configuration = JObject.Parse(jsonString);
            configuration.AccessToken = token;

            var newJsonString = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(_configurationFilePath, newJsonString);
        }
    }
}
