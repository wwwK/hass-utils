using HassUtils.Shared.Configuration;
using NUnit.Framework;

namespace HassUtils.T1.Tests.Configuration
{
  [TestFixture]
  public class HassUtilsConfigTests
  {
    [Test]
    public void HassUtilsConfig_Given_Constructed_ShouldDefault_Server()
    {
      // arrange
      var config = new HassUtilsConfig();

      // assert
      Assert.IsNotNull(config.Server);
      Assert.IsInstanceOf<HomeAssistantServerConfig>(config.Server);
    }
  }
}
