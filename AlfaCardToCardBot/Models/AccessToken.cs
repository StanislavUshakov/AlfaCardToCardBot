using Newtonsoft.Json;

namespace AlfaCardToCardBot.Models
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}