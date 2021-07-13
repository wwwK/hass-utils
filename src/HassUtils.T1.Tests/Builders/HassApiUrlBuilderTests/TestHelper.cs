using HassUtils.Shared.Builders;
using HassUtils.Shared.Configuration;
using HassUtils.T1.Tests.TestSupport.Builders.Configuration;
using NSubstitute;
using Rn.NetCore.Common.Logging;

namespace HassUtils.T1.Tests.Builders.HassApiUrlBuilderTests
{
  public static class TestHelper
  {
    public static HassApiUrlBuilder GetApiUrlBuilder(
      ILoggerAdapter<HassApiUrlBuilder> logger = null,
      HassUtilsConfig hassUtilsConfig = null)
    {
      return new HassApiUrlBuilder(
        logger ?? Substitute.For<ILoggerAdapter<HassApiUrlBuilder>>(),
        hassUtilsConfig ?? new HassUtilsConfigBuilder().WithDefaults().Build()
      );
    }
  }
}
