using System;
using System.Linq;

namespace HotBot.Plugins.Lottery
{
	public sealed class LotteryWinnerEvent
	{
		public Lottery Lottery { get; }

		public LotteryWinnerEvent(Lottery lottery)
		{
			if (lottery == null)
			{
				throw new ArgumentNullException("lottery");
			}
			Lottery = lottery;
		}
	}
}