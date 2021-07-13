using System.Text;
using HassUtils.Shared.Configuration;
using Rn.NetCore.Common.Logging;

namespace HassUtils.Shared.Builders
{
  public interface IHassApiUrlBuilder
  {
    string ApiBaseUrl { get; }
  }

  public class HassApiUrlBuilder : IHassApiUrlBuilder
  {
    public string ApiBaseUrl { get; private set; }

    private readonly ILoggerAdapter<HassApiUrlBuilder> _logger;

    public HassApiUrlBuilder(
      ILoggerAdapter<HassApiUrlBuilder> logger,
      HassUtilsConfig hassUtilsConfig)
    {
      _logger = logger;

      ApiBaseUrl = BuildApiBaseUrl(hassUtilsConfig.Server);

      _logger.Info("Setting base API url to {url}", ApiBaseUrl);
    }

    private static string BuildApiBaseUrl(HomeAssistantServerConfig config)
    {
      return new StringBuilder()
        .Append(config.UseHttps ? "https://" : "http://")
        .Append(config.Host)
        .Append(":")
        .Append(config.Port)
        .Append("/api")
        .ToString();
    }
  }
}
