using System;
using HassUtils.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Logging;

namespace HassUtils
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = LogManager.GetCurrentClassLogger();

      try
      {
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Stopped program because of exception");
        throw;
      }
      finally
      {
        LogManager.Shutdown();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
          services
            // Configuration
            .AddSingleton(BindHassUtilsConfig(hostContext.Configuration))

            // Logging
            .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
            .AddLogging(loggingBuilder =>
            {
              // configure Logging with NLog
              loggingBuilder.ClearProviders();
              loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
              loggingBuilder.AddNLog(hostContext.Configuration);
            })

            // Worker
            .AddHostedService<Worker>();
        });

    private static HassUtilsConfig BindHassUtilsConfig(IConfiguration configuration)
    {
      var boundConfig = new HassUtilsConfig();
      var configSection = configuration.GetSection(HassUtilsConfig.ConfigKey);
      
      if(configSection.Exists())
        configSection.Bind(boundConfig);

      return boundConfig;
    }
  }
}
