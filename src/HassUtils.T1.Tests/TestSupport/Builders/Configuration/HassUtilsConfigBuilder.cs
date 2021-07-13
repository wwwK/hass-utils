using HassUtils.Shared.Configuration;

namespace HassUtils.T1.Tests.TestSupport.Builders.Configuration
{
  public class HassUtilsConfigBuilder
  {
    private readonly HassUtilsConfig _config;

    public HassUtilsConfigBuilder()
    {
      _config = new HassUtilsConfig();
    }

    public HassUtilsConfigBuilder WithDefaults()
    {
      _config.Server = new HomeAssistantServerConfigBuilder()
        .WithDefaults()
        .Build();

      return this;
    }

    public HassUtilsConfigBuilder WithServerConfig(HomeAssistantServerConfig server)
    {
      _config.Server = server;
      return this;
    }

    public HassUtilsConfig Build() => _config;
  }
}
