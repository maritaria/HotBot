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
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}

			Connection = connection;
			Response = response;
		}
	}
}