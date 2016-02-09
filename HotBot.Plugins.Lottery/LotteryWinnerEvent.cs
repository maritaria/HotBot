using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
