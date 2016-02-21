using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Irc.Impl;
using HotBot.Core.Plugins;
using HotBot.Plugins.Lottery;
using HotBot.Plugins.Wallet;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

[assembly: Plugin(typeof(LotteryPlugin))]
[assembly: PluginDependency(typeof(WalletPlugin))]

namespace HotBot.Plugins.Lottery
{
	public sealed class LotteryPlugin : Plugin
	{
		public const string LotteryCurrency = "lottery";

		public PluginDescription Description { get; } = new PluginDescription("Lottery", "Hosts lotteryies and rewards money to winners");

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public CommandManager CommandManager { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		[Dependency]
		public WalletPlugin Wallets { get; set; }

		public Lottery CurrentLottery { get; private set; }

		public void Load()
		{
			Bus.Subscribe(this);
			CommandManager.Register(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
			CommandManager.Unregister(this);
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
			//Bus.PublishSpecific(new ChatTransmitRequest(message.Lottery.Channel, $"Lottery finished, the winner is {message.Lottery.Winner.Name}!"));
			var value = Wallets.GetCurrency(message.Lottery.Winner, LotteryCurrency);
			Wallets.SetCurrency(message.Lottery.Winner, LotteryCurrency, value + message.Lottery.Pot);
			//Bus.PublishSpecific(new ChatTransmitRequest(message.Lottery.Channel, $"{User.HandlePrefix}{message.Lottery.Winner.Name} Congrats, you have won {message.Lottery.Pot} blorps"));
		}

		[Command("joinlottery")]
		public void JoinLotteryCommand(CommandEvent info)
		{
			if (CurrentLottery == null)
			{
				//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"@{info.User.Name}, there is no lottery right now :("));
			}
			else
			{
				if (CurrentLottery.Participants.Contains(info.User))
				{
					//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $""));
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
						//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"@{info.User.Name} ERROR: {ex.Message}"));
						return;
					}
					if (success)
					{
						//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, $"New lottery participant: (@{info.User.Name})"));
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
				//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A new lottery has been started. You have 1 minute to type !joinlottery to participate :D"));
			}
			else
			{
				//Bus.PublishSpecific(new ChatTransmitRequest(info.Channel, "A lottery is already running"));
			}
		}
	}
}