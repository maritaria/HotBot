using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using System;
using System.Linq;

namespace HotBot.Plugin.Lottery
{
	public class GetBalanceListener : LotteryCommandListener
	{
		public MessageBus Bus { get; }

		public GetBalanceListener(LotteryController controller, MessageBus bus, CommandRedirecter redirecter) : base(controller)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			redirecter.AddListener("balance", this);
		}

		public override void OnCommand(CommandEvent info)
		{
			string message = $"{User.HandlePrefix}{info.User.Name} you have {info.User.Money} blorps";
			Bus.Publish(new ChatTransmitRequest(info.Channel, message));
		}
	}
}