using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Reflection;
using TwitchDungeon.Services.Commands;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Irc;
using TwitchDungeon.Services;

namespace TwitchDungeon
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var container = new UnityContainer();

			container.RegisterInstance(typeof(IUnityContainer), container);

			//TODO: 2 options
			//1. reflection the fuck out of all the types and figure out which to pass to RegisterAll()
			//2. Use initalizer types to initialize groups of types in a bunch at a time :)

			//container.RegisterTypes(Assembly.GetExecutingAssembly().GetTypes().Where()

			container.RegisterType<CommandEncoder, CommandEncoder>(new ContainerControlledLifetimeManager());
			container.RegisterType<DataStore, DbContextDataStore>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcClient, IrcClient>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcLogger, IrcLogger>(new ContainerControlledLifetimeManager());
			container.RegisterType<MessageBus, DictionaryMessageBus>(new ContainerControlledLifetimeManager());
			container.RegisterType<TwitchBot, TwitchBot>(new ContainerControlledLifetimeManager());

			container.RegisterType<CommandListener, SimpleCommandHandler>(new ContainerControlledLifetimeManager());
			
			var bot = container.Resolve<TwitchBot>();

		}
	}
}