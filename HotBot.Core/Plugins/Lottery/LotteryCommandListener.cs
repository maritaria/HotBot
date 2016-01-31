using System;
using System.Linq;
using TwitchDungeon.Services.Commands;

namespace TwitchDungeon.Plugins.Lottery
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

		public abstract void OnCommand(CommandInfo info);
	}
}