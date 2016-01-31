using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;

namespace HotBot.Plugin.Lottery
{
	public class JoinLotteryListener : LotteryCommandListener
	{
		public MessageBus Bus { get; }

		public JoinLotteryListener(LotteryController controller, MessageBus bus, CommandRedirecter redirecter) : base(controller)
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
			redirecter.AddListener("joinlottery", this);
		}

		public override void OnCommand(CommandInfo info)
		{
			if (Controller.CurrentLottery == null)
			{
				Bus.Publish(new SendChatMessage(info.Channel, $"@{info.User.Name}, there is no lottery right now :("));
			}
			else
			{
				if (Controller.CurrentLottery.Participants.Contains(info.User))
				{
					Bus.Publish(new SendChatMessage(info.Channel, $""));
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
						Bus.Publish(new SendChatMessage(info.Channel, $"@{info.User.Name} ERROR: {ex.Message}"));
						return;
					}
					if (success)
					{
						Bus.Publish(new SendChatMessage(info.Channel, $"New lottery participant: (@{info.User.Name})"));
					}
				}
			}
		}
	}
}