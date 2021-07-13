using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HassUtils.Shared.Configuration
{
  public class HassUtilsConfig
  {
    [JsonProperty("Server"), JsonPropertyName("Server")]
    public HomeAssistantServerConfig Server { get; set; }

    public HassUtilsConfig()
    {
      Server = new HomeAssistantServerConfig();
    }
  }
}
