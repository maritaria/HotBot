using System;
using System.Linq;

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