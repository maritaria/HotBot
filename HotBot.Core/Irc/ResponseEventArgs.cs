using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ResponseEventArgs : EventArgs
	{
		public IrcConnection Connection { get; }
		public Response Response { get; }

		public ResponseEventArgs(IrcConnection connection, Response response)
		{
			Verify.NotNull(connection, "connection");
			Verify.NotNull(response, "response");
			Connection = connection;
			Response = response;
		}
	}
}