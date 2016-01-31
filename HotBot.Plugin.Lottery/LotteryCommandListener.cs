using System;
using System.Linq;
using HotBot.Core.Commands;

namespace HotBot.Plugin.Lottery
{
	public abstract class LotteryCommandListener : CommandListener
	{
		public LotteryController Controller { get; }

		public LotteryCommandListener(LotteryController controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			Controller = controller;
		}

		public abstract void OnCommand(CommandEvent info);
	}
}