using System;
using System.IO;
using HassUtils.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Encryption;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;

namespace HassUtils.DevConsole
{
  class Program
  {
    private static IServiceProvider _serviceProvider;
    private static ILoggerAdapter<Program> _logger;

    static void Main(string[] args)
    {
      ConfigureDI();

      Console.WriteLine("Hello World!");
    }


    // DI related methods
    private static void ConfigureDI()
    {
      var services = new ServiceCollection();

      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

      services
        // Configuration
        .AddSingleton<IConfiguration>(config)
        .AddSingleton(BindHassUtilsConfig(config))

        // Logging
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
        .AddLogging(loggingBuilder =>
        {
          // configure Logging with NLog
          loggingBuilder.ClearProviders();
          loggingBuilder.SetMinimumLevel(LogLevel.Trace);
          loggingBuilder.AddNLog(config);
        })

        // Metrics
        .AddSingleton<IMetricService, MetricService>();

      _serviceProvider = services.BuildServiceProvider();
      _logger = _serviceProvider.GetService<ILoggerAdapter<Program>>();
    }

    private static HassUtilsConfig BindHassUtilsConfig(IConfiguration configuration)
    {
      var boundConfig = new HassUtilsConfig();
      var configSection = configuration.GetSection(HassUtilsConfig.ConfigKey);

      if (configSection.Exists())
        configSection.Bind(boundConfig);

      return boundConfig;
    }
  }
}
