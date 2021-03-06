﻿using System;
using System.IO;
using BitbankApiTool.Commands;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitbankApiTool
{
    /// <summary>
    /// サンプル
    /// </summary>
    class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリーポイントです。
        /// </summary>
        /// <param name="args">引数</param>
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var application = new CommandLineApplication<Command>();
            application.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);

            try
            {
                application.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddConsole());

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("setting.json", true)
                .Build();

            services.AddOptions();
            services.Configure<Setting>(configuration);
            services.AddTransient<IBitbankApiToolService, BitbankApiToolService>();
        }
    }
}