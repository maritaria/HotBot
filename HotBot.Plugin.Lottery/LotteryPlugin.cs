using HotBot.Core;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Plugin.Lottery
{
	public sealed class LotteryPlugin : LoadablePlugin
	{
		public PluginManager Manager { get; }
		public MessageBus Bus { get; }
		public PluginDescription Description { get; }

		public LotteryPlugin(PluginManager manager, MessageBus bus)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Manager = manager;
			Bus = bus;
			Description = new PluginDescription("Lottery", "Hosts lotteries and rewards money to winners");
		}

		public void Load()
		{
		}

		public void Unload()
		{
		}

		public static void Bootstrap(IUnityContainer container)
		{
			//TODO: from config?
			container.RegisterType<Lottery, Lottery>(new PerResolveLifetimeManager());
			container.RegisterType<LotteryController, LotteryController>(new ContainerControlledLifetimeManager());
			container.RegisterType<JoinLotteryListener, JoinLotteryListener>(new ContainerControlledLifetimeManager());
			container.RegisterType<StartLotteryListener, StartLotteryListener>(new ContainerControlledLifetimeManager());
			
			container.Resolve<LotteryController>();
			container.Resolve<StartLotteryListener>();
			container.Resolve<JoinLotteryListener>();
			container.Resolve<GetBalanceListener>();
		}
	}
}