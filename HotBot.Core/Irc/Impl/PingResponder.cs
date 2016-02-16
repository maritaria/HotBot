using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
