using Microsoft.Practices.Unity;
using System;
using System.Linq;
using TwitchDungeon.Services;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Irc;

namespace TwitchDungeon.Plugins.Lottery
{
	public class LotteryController : MessageHandler<LotteryWinnerFound>
	{
		public MessageBus Bus { get; }
		public Lottery CurrentLottery { get; private set; }
		public IUnityContainer UnityContainer { get; }

		public LotteryController(IUnityContainer container, MessageBus bus)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			UnityContainer = container;
			Bus = bus;
			Bus.Subscribe(this);
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
			return UnityContainer.Resolve<Lottery>();
		}

		void MessageHandler<LotteryWinnerFound>.HandleMessage(LotteryWinnerFound message)
		{
			CurrentLottery = null;
			Bus.Publish(new SendChatMessage(message.Lottery.Channel, $"Lottery finished, the winner is {message.Lottery.Winner.Username}!"));
			message.Lottery.Winner.Money += message.Lottery.Pot;
			Bus.Publish(new SaveChanges());
		}
	}
}