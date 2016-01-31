using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core
{
	public sealed class ThrottleController
	{
		List<int> _history = new List<int>();
		int _pressure = 0;
		DateTime _lastPressure;

		public int CommandLimit { get; set; } = 20;
		public TimeSpan CommandLimitReach { get; set; } = TimeSpan.FromSeconds(25);

		public void AddPressure(int weight)
		{

		}

		public void Throttle(int upcommingWeight)
		{

		}

	}
}
