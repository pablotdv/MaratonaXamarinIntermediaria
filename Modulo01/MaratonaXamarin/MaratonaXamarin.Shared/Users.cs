using Newtonsoft.Json;

namespace MaratonaXamarin.Shared
{
    public class Users
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
