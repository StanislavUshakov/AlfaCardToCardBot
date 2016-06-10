using Newtonsoft.Json;

namespace AlfaCardToCardBot.Models
{
    public class FeeResponse
    {
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        public string interest { get; set; }
        public string constant { get; set; }
        public string min { get; set; }
        public string max { get; set; }
    }
}