using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class PingResponder
	{
		public IrcConnection Connection { get; }

		public PingResponder(IrcConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			Connection = connection;
		}
	}
}