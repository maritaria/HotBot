using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			UnityContainer container = CreateContainer();
			InitializeTypes(container);
			InitializeConfig(container);
			InitializeInstances(container);
		}

		private static UnityContainer CreateContainer()
		{
			var container = new UnityContainer();
			container.RegisterInstance(typeof(IUnityContainer), container, new ContainerControlledLifetimeManager());
			return container;
		}

		private static void InitializeTypes(UnityContainer container)
		{
			container.RegisterType<CommandEncoder, CommandEncoder>(new ContainerControlledLifetimeManager());
			container.RegisterType<CommandListener, SimpleCommandHandler>(new ContainerControlledLifetimeManager());
			container.RegisterType<DataStore, DbContextDataStore>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcClient, IrcClient>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcLogger, IrcLogger>(new ContainerControlledLifetimeManager());
			container.RegisterType<MessageBus, DictionaryMessageBus>(new ContainerControlledLifetimeManager());
			container.RegisterType<PluginManager, ReflectionPluginManager>(new ContainerControlledLifetimeManager());
			container.RegisterType<TwitchBot, TwitchBot>(new ContainerControlledLifetimeManager());
		}

		private static void InitializeConfig(UnityContainer container)
		{
			MasterConfig config = new MasterConfig();
			container.RegisterInstance(config, new ContainerControlledLifetimeManager());
			container.RegisterInstance<IrcClientConfig>(config, new ExternallyControlledLifetimeManager());
		}

		private static void InitializeInstances(UnityContainer container)
		{
			container.Resolve<PrivateMessageDecoder>();
			container.Resolve<IrcLogger>();
			container.Resolve<CommandEncoder>();
			container.Resolve<PluginManager>();
			container.Resolve<TwitchBot>();
		}
	}
}