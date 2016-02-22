using HotBot.Core;
using HotBot.Core.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using HotBot.Core.Util;
using HotBot.Core.Unity;

namespace HotBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var container = CreateContainer();
			container.RegisterTypes(new HotBotRegistrationConvention(Assembly.GetExecutingAssembly()));
			container.RegisterTypes(new HotBotRegistrationConvention(typeof(HotBotRegistrationConvention).Assembly));
			ApplyUnityConfig(container);
			InitializeMasterConfig(container);
			InitializeInstances(container);
		}

		private static IUnityContainer CreateContainer()
		{
			var container = new UnityContainer();
			container.RegisterInstance(typeof(IUnityContainer), container, new ContainerControlledLifetimeManager());
			return container;
		}

		private static void ApplyUnityConfig(IUnityContainer container)
		{
			var section = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
			section.Configure(container);
		}

		private static void InitializeMasterConfig(IUnityContainer container)
		{
			MasterConfig config = new MasterConfig();
			container.RegisterInstance(config, new ContainerControlledLifetimeManager());
			container.RegisterInstance<CommandManagerConfig>(config, new ExternallyControlledLifetimeManager());
		}

		private static void InitializeInstances(IUnityContainer container)
		{
			container.Resolve<ChatCommandScanner>();
			var bot = container.Resolve<TwitchBot>();
			bot.Run();
		}
	}
}