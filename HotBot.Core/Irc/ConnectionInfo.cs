using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public struct ConnectionInfo
	{
		public string Hostname;
		public ushort Port;

		public ConnectionInfo(string source)
		{
			string[] parts = source.SplitOnce(":");
			Hostname = parts[0];
			Port = ushort.Parse(parts[1]);
		}

	}
}
