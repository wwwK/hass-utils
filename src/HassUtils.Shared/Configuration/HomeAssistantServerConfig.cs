using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HassUtils.Shared.Configuration
{
  public class HomeAssistantServerConfig
  {
    [JsonProperty("UseHttps"), JsonPropertyName("UseHttps")]
    public bool UseHttps { get; set; }

    [JsonProperty("Host"), JsonPropertyName("Host")]
    public string Host { get; set; }

    [JsonProperty("Port"), JsonPropertyName("Port")]
    public int Port { get; set; }

    [JsonProperty("AuthToken"), JsonPropertyName("AuthToken")]
    public string AuthToken { get; set; }

    [JsonProperty("SkipCertValidation"), JsonPropertyName("SkipCertValidation")]
    public bool SkipCertValidation { get; set; }

    public HomeAssistantServerConfig()
    {
      UseHttps = false;
      Host = "hassio.local";
      Port = 8123;
      AuthToken = string.Empty;
      SkipCertValidation = false;
    }
  }
}
