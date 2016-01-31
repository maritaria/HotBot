using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public sealed class Throttler
	{
		//TODO: Implement this class
		List<int> _history = new List<int>();
		int _pressure = 0;

		public int Limit { get; set; } = 20;
		public TimeSpan LimitCooldown { get; set; } = TimeSpan.FromSeconds(25);

		public void AddPressure(int weight)
		{

		}

		public void Throttle(int upcommingWeight)
		{

		}

	}
}
