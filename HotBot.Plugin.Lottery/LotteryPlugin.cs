using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Plugin.Lottery
{
	public sealed class LotteryPlugin : BootstrappedPlugin
	{
		public PluginManager Manager { get; }
		public MessageBus Bus { get; }
		public PluginDescription Description { get; }
		public CommandRedirecter Redirecter { get; }
		public LotteryController Controller { get; }

		public LotteryPlugin(PluginManager manager, MessageBus bus, CommandRedirecter commandRedirecter, LotteryController controller)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (commandRedirecter == null)
			{
				throw new ArgumentNullException("commandRedirecter");
			}
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			Manager = manager;
			Bus = bus;
			Description = new PluginDescription("Lottery", "Hosts lotteries and rewards money to winners");
			Redirecter = commandRedirecter;
			Controller = controller;
		}

		public void Load()
		{
			Bus.PublishSpecific(new RegisterPluginCommandsRequest(this));
		}

		public void Unload()
		{
			Bus.PublishSpecific(new UnregisterPluginCommandsRequest(this));
		}

		public void Bootstrap(IUnityContainer container)
		{
			container.RegisterType<Lottery, Lottery>(new PerResolveLifetimeManager());
			container.RegisterType<LotteryController, LotteryController>(new ContainerControlledLifetimeManager());
			container.Resolve<LotteryController>();
		}

		[PluginCommand("joinlottery")]
		public void JoinLotteryCommand(CommandEvent info)
		{
			if (Controller.CurrentLottery == null)
			{
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"@{info.User.Name}, there is no lottery right now :("));
			}
			else
			{
				if (Controller.CurrentLottery.Participants.Contains(info.User))
				{
					Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $""));
				}
				else
				{
					bool success = true;
					try
					{
						Controller.CurrentLottery.Join(info.User);
					}
					catch (LotteryException ex)
					{
						success = false;
						Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"@{info.User.Name} ERROR: {ex.Message}"));
						return;
					}
					if (success)
					{
						Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"New lottery participant: (@{info.User.Name})"));
					}
				}
			}
		}

		[PluginCommand("startlottery")]
		public void StartLotteryCommand(CommandEvent info)
		{
			if (Controller.CurrentLottery == null)
			{
				Controller.CreateLottery();
				Controller.CurrentLottery.Pot = 1000;
				Controller.CurrentLottery.Duration = TimeSpan.FromMinutes(1);
				Controller.CurrentLottery.Start(info.Channel);
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A new lottery has been started. You have 1 minute to type !joinlottery to participate :D"));
			}
			else
			{
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A lottery is already running"));
			}
		}

		[PluginCommand("money")]
		public void GetBalanceCommand(CommandEvent info)
		{
			string message = $"{User.HandlePrefix}{info.User.Name} you have {info.User.Money} blorps";
			Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, message));
		}
	}
}