using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Plugins.Lottery
{
	//TODO: public static bootstrapper class, maybe something with attributes again
	public sealed class LotteryPlugin : BootstrappedPlugin
	{
		public PluginDescription Description { get; } = new PluginDescription("Lottery", "Hosts lotteryies and rewards money to winners");

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public CommandManager CommandManager { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		public Lottery CurrentLottery { get; private set; }

		public void Load()
		{
			CommandManager.Register(this);
		}

		public void Unload()
		{
			CommandManager.Unregister(this);
		}

		public void Bootstrap(IUnityContainer container)
		{
			container.RegisterType<Lottery, Lottery>(new PerResolveLifetimeManager());
		}

		public void CreateLottery()
		{
			if (CurrentLottery == null)
			{
				CurrentLottery = CreateLotteryInternal();
			}
			else
			{
				switch (CurrentLottery.State)
				{
					case LotteryState.Open:
						throw new LotteryException("A lottery already running");
					case LotteryState.Finished:
						CurrentLottery = CreateLotteryInternal();
						break;
				}
			}
		}

		private Lottery CreateLotteryInternal()
		{
			return new Lottery(Bus);
		}

		[Subscribe]
		public void OnLotteryWinner(LotteryWinnerEvent message)
		{
			CurrentLottery = null;
			Bus.PublishSpecific(new ChatTransmitRequest(message.Lottery.Channel, $"Lottery finished, the winner is {message.Lottery.Winner.Name}!"));
			message.Lottery.Winner.Money += message.Lottery.Pot;
			Bus.PublishSpecific(new ChatTransmitRequest(message.Lottery.Channel, $"{User.HandlePrefix}{message.Lottery.Winner.Name} Congrats, you have won {message.Lottery.Pot} blorps"));
			Bus.PublishSpecific(new SaveDatabaseChangesRequest());
		}

		[Command("joinlottery")]
		public void JoinLotteryCommand(CommandEvent info)
		{
			if (CurrentLottery == null)
			{
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"@{info.User.Name}, there is no lottery right now :("));
			}
			else
			{
				if (CurrentLottery.Participants.Contains(info.User))
				{
					Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $""));
				}
				else
				{
					bool success = true;
					try
					{
						CurrentLottery.Join(info.User);
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

		[Command("startlottery")]
		public void StartLotteryCommand(CommandEvent info)
		{
			if (CurrentLottery == null)
			{
				CreateLottery();
				CurrentLottery.Pot = 1000;
				CurrentLottery.Duration = TimeSpan.FromMinutes(1);
				CurrentLottery.Start(info.Channel);
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A new lottery has been started. You have 1 minute to type !joinlottery to participate :D"));
			}
			else
			{
				Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A lottery is already running"));
			}
		}

		[Command("money")]
		public void GetBalanceCommand(CommandEvent info)
		{
			string message = $"{User.HandlePrefix}{info.User.Name} you have {info.User.Money} blorps";
			Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, message));
		}
	}
}