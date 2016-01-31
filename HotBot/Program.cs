using HotBot.Core;
using HotBot.Core.Plugins.Lottery;
using HotBot.Core.Services;
using HotBot.Core.Services.Commands;
using HotBot.Core.Services.DataStorage;
using HotBot.Core.Services.Irc;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new UnityContainer();

			container.RegisterInstance(typeof(IUnityContainer), container);

			container.RegisterType<CommandEncoder, CommandEncoder>(new ContainerControlledLifetimeManager());
			container.RegisterType<DataStore, DbContextDataStore>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcClient, IrcClient>(new ContainerControlledLifetimeManager());
			container.RegisterType<IrcLogger, IrcLogger>(new ContainerControlledLifetimeManager());
			container.RegisterType<MessageBus, DictionaryMessageBus>(new ContainerControlledLifetimeManager());
			container.RegisterType<TwitchBot, TwitchBot>(new ContainerControlledLifetimeManager());
			container.RegisterType<CommandListener, SimpleCommandHandler>(new ContainerControlledLifetimeManager());

			container.RegisterType<Lottery, Lottery>(new PerResolveLifetimeManager());
			container.RegisterType<LotteryController, LotteryController>(new ContainerControlledLifetimeManager());
			container.RegisterType<JoinLotteryListener, JoinLotteryListener>(new ContainerControlledLifetimeManager());
			container.RegisterType<StartLotteryListener, StartLotteryListener>(new ContainerControlledLifetimeManager());

			container.Resolve<CommandEncoder>();
			container.Resolve<LotteryController>();
			container.Resolve<StartLotteryListener>();
			container.Resolve<JoinLotteryListener>();

			container.Resolve<TwitchBot>();
		}
	}
}
