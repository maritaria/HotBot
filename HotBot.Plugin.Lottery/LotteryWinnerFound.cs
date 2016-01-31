using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Plugin.Lottery
{
	public sealed class LotteryWinnerFound
	{
		public Lottery Lottery { get; }

		public LotteryWinnerFound(Lottery lottery)
		{
			if (lottery == null)
			{
				throw new ArgumentNullException("lottery");
			}
			Lottery = lottery;
		}

	}
}
