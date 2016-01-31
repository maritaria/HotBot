using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;

namespace HotBot.Plugin.Lottery
{
	public class StartLotteryListener : LotteryCommandListener
	{
		public MessageBus Bus { get; }

		public StartLotteryListener(LotteryController controller, MessageBus bus, CommandRedirecter redirecter) : base(controller)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (redirecter == null)
			{
				throw new ArgumentNullException("redirecter");
			}
			Bus = bus;
			redirecter.AddListener("startlottery", this);
		}

		public override void OnCommand(CommandEvent info)
		{
			if (Controller.CurrentLottery == null)
			{
				Controller.CreateLottery();
				Controller.CurrentLottery.Pot = 1000;
				Controller.CurrentLottery.Duration = TimeSpan.FromSeconds(30);
				Controller.CurrentLottery.Start(info.Channel);
				Bus.Publish(new ChatTransmitEvent(info.Channel, "A new lottery has been started. Type !joinlottery to participate :D"));
			}
			else
			{
				Bus.Publish(new ChatTransmitEvent(info.Channel, "A lottery is already running"));
			}
		}
	}
}