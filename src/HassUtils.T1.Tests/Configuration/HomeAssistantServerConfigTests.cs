using HassUtils.Shared.Configuration;
using NUnit.Framework;

namespace HassUtils.T1.Tests.Configuration
{
  [TestFixture]
  public class HomeAssistantServerConfigTests
  {
    [Test]
    public void HomeAssistantServerConfig_Given_Constructed_ShouldDefault_UseHttps()
    {
      Assert.IsFalse(new HomeAssistantServerConfig().UseHttps);
    }

    [Test]
    public void HomeAssistantServerConfig_Given_Constructed_ShouldDefault_Host()
    {
      Assert.AreEqual(
        "hassio.local",
        new HomeAssistantServerConfig().Host
      );
    }

    [Test]
    public void HomeAssistantServerConfig_Given_Constructed_ShouldDefault_Port()
    {
      Assert.AreEqual(
        8123,
        new HomeAssistantServerConfig().Port
      );
    }

    [Test]
    public void HomeAssistantServerConfig_Given_Constructed_ShouldDefault_AuthToken()
    {
      Assert.AreEqual(
        string.Empty,
        new HomeAssistantServerConfig().AuthToken
      );
    }

    [Test]
    public void HomeAssistantServerConfig_Given_Constructed_ShouldDefault_SkipCertValidation()
    {
      Assert.IsFalse(new HomeAssistantServerConfig().SkipCertValidation);
    }
  }
}
