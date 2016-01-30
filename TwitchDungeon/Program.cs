using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Reflection;
using TwitchDungeon.Services.Commands;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Irc;
using TwitchDungeon.Services.Messages;

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

			container.RegisterType(typeof(CommandHandlerService), typeof(CommandHandlerService), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(DataService), typeof(DataService), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(IrcClient), typeof(IrcClient), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(MessageBus), typeof(MessageBus), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(TwitchBot), typeof(TwitchBot), new ContainerControlledLifetimeManager());

			container.ResolveAll(typeof(CommandHandler));

			var bot = container.Resolve<TwitchBot>();

		}
	}
}