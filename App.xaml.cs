﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Windows;
using DesignIntentDesktop.Components.DocumentDisplay;
using DesignIntentDesktop.Helpers;
using DesignIntentDesktop.ServiceProxies;
using DesignIntentDesktop.services.Authentication;
using DesignIntentDesktop.services.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesignIntentDesktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IServiceProvider ServiceProvider { get; private set; }
		
		public IConfiguration Configuration { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			Configuration = builder.Build();

			var serviceCollection = new ServiceCollection();
			
			ConfigureServices(serviceCollection);
			ConfigureHttp(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();

			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

			services.AddScoped(typeof(CloudFileViewModel));
			services.AddTransient(typeof(MainWindow));
			services.AddTransient<AuthenticationDelegationHandler>();
		}

		public void ConfigureHttp(IServiceCollection services)
		{
			services.AddHttpClient<ICloudFileService, CloudFileService>(x => 
			{
				x.BaseAddress = new Uri(Configuration.GetSetting<string>(nameof(AppSettings.DesignIntentUrl)));
			}).AddHttpMessageHandler<AuthenticationDelegationHandler>();

            services.AddHttpClient<IAuthService, AuthService>(x =>
			{
				x.BaseAddress = new Uri(Configuration.GetSetting<string>(nameof(AppSettings.DesignIntentUrl)) + "api/");
				x.DefaultRequestHeaders.Accept.Clear();
				x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			});

			services.AddHttpClient<ICloudStorageService, CloudStorageService>(x =>
			{
				x.BaseAddress = new Uri(Configuration.GetSetting<string>(nameof(AppSettings.DesignIntentUrl)));
			});
		}

		public App()
        {
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY5MjI2QDMxMzgyZTM0MmUzMFNDSzF3MXpvR3o3Nmloc2lUYVFqVEFaekVoL0h0dmg2bDVlcDZvcGorRTA9");
		}
	}
}
