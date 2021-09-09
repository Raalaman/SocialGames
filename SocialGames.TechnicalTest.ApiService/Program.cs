using ApplicationCore;
using AutoMapper;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialGames.TechnicalTest.Games.Games.Services;
using SocialGames.TechnicalTest.Games.Games.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SocialGames.TechnicalTest.ApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder().Build().Run();
        }


        private static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>return <see cref="IHost"/> with all the services loaded</returns>
        private static IHostBuilder CreateHostBuilder()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);
            var configuration = builder.Build();

            //Load settings from appsettings.json
            SocialGamesSettings settings = new SocialGamesSettings();
            configuration.GetSection(SocialGamesSettings.SECTION).Bind(settings);

            return Host.CreateDefaultBuilder()
                        .UseServiceProviderFactory(new LightInjectServiceProviderFactory())
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        })
                        .ConfigureServices((context, services) =>
                        {
                            services.AddSingleton(typeof(IGameService), typeof(GameService));
                        })
                        .UseSerilog((hostingContext, loggerConfig) =>
                        loggerConfig.ReadFrom.Configuration(hostingContext.Configuration))                        ;

        }
    }
}
