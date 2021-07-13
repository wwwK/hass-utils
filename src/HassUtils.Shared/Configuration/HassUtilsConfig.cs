using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HassUtils.Shared.Configuration
{
  public class HassUtilsConfig
  {
    public const string ConfigKey = "HassUtils";

    [JsonProperty("Server"), JsonPropertyName("Server")]
    public HomeAssistantServerConfig Server { get; set; }

    public HassUtilsConfig()
    {
      Server = new HomeAssistantServerConfig();
    }
  }
}
