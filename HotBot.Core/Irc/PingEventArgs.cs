using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class PingEventArgs : EventArgs
	{
		public IrcConnection Connection { get; }

		public PingEventArgs(IrcConnection connection)
		{
			Verify.NotNull(connection, "connection");
			Connection = connection;
		}
	}
}