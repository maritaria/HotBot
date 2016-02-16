using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public sealed class PingEventArgs : EventArgs
	{
		public IrcConnection Connection { get; }

		public PingEventArgs(IrcConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			Connection = connection;
		}

	}
}
