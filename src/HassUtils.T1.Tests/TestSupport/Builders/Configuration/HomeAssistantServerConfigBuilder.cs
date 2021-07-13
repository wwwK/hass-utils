using HassUtils.Shared.Configuration;

namespace HassUtils.T1.Tests.TestSupport.Builders.Configuration
{
  public class HomeAssistantServerConfigBuilder
  {
    private readonly HomeAssistantServerConfig _config;

    public HomeAssistantServerConfigBuilder()
    {
      _config = new HomeAssistantServerConfig();
    }

    public HomeAssistantServerConfigBuilder WithDefaults()
    {
      _config.UseHttps = false;
      _config.Host = "hassio.local";
      _config.Port = 8123;
      _config.AuthToken = string.Empty;
      _config.SkipCertValidation = false;

      return this;
    }

    public HomeAssistantServerConfigBuilder WithPort(int port)
    {
      _config.Port = port;
      return this;
    }

    public HomeAssistantServerConfigBuilder WithHost(string host)
    {
      _config.Host = host;
      return this;
    }

    public HomeAssistantServerConfigBuilder WithHost(string host, int port, bool useHttps = false)
      => WithHost(host).WithPort(port).WithUseHttps(useHttps);

    public HomeAssistantServerConfigBuilder WithUseHttps(bool useHttps)
    {
      _config.UseHttps = useHttps;
      return this;
    }

    public HomeAssistantServerConfig Build() => _config;
  }
}
