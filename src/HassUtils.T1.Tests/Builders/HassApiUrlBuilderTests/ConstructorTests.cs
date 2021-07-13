using HassUtils.Shared.Builders;
using HassUtils.Shared.Configuration;
using HassUtils.T1.Tests.TestSupport.Builders.Configuration;
using NSubstitute;
using NUnit.Framework;
using Rn.NetCore.Common.Logging;

namespace HassUtils.T1.Tests.Builders.HassApiUrlBuilderTests
{
  [TestFixture]
  public class ConstructorTests
  {
    [Test]
    public void Constructor_Given_Called_ShouldBuild_ApiBaseUrl()
    {
      // arrange
      var config = GetDefaultConfig();

      // act
      var builder = TestHelper.GetApiUrlBuilder(hassUtilsConfig: config);

      // assert
      Assert.AreEqual(
        "http://127.0.0.1:8123/api",
        builder.ApiBaseUrl
      );
    }

    [Test]
    public void Constructor_Given_Called_ShouldLog_ApiBaseUrl()
    {
      // arrange
      var logger = Substitute.For<ILoggerAdapter<HassApiUrlBuilder>>();
      var config = GetDefaultConfig();

      // act
      TestHelper.GetApiUrlBuilder(
        hassUtilsConfig: config,
        logger: logger
      );

      // assert
      logger.Received(1).Info(
        "Setting base API url to {url}",
        "http://127.0.0.1:8123/api"
      );
    }


    // Internal
    private static HassUtilsConfig GetDefaultConfig()
    {
      return new HassUtilsConfigBuilder()
        .WithDefaults()
        .WithServerConfig(new HomeAssistantServerConfigBuilder()
          .WithDefaults()
          .WithHost("127.0.0.1", 8123)
          .Build())
        .Build();
    }
  }
}
