using Microsoft.Practices.Unity;
using System;
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

			container.RegisterType(typeof(CommandHandlerService), typeof(CommandHandlerService), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(DataService), typeof(DataService), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(IrcClient), typeof(IrcClient), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(MessageBus), typeof(MessageBus), new ContainerControlledLifetimeManager());
			container.RegisterType(typeof(TwitchBot), typeof(TwitchBot), new ContainerControlledLifetimeManager());

			var bot = container.Resolve<TwitchBot>();
		}
	}
}