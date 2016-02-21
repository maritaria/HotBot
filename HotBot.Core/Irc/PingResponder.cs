using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class PingResponder
	{
		public const string PingCommand = "PING";
		public const string PingReplyCommand = "PONG";

		public IrcConnection Connection { get; }

		public PingResponder(IrcConnection connection)
		{
			Verify.NotNull(connection, "connection");
			Connection = connection;
			Connection.ResponseReceived += Connection_ResponseReceived;
		}
		
		private void Connection_ResponseReceived(object sender, ResponseEventArgs e)
		{
			if (e.Response.Command.Equals(PingCommand, StringComparison.OrdinalIgnoreCase))
			{
				SendPong(e.Connection, e.Response.HostMask);
			}
		}

		private void SendPong(IrcConnection connection, HostMask hostMask)
		{
			if (hostMask == null)
			{
				connection.SendCommand(PingReplyCommand);
			}
			else
			{
				connection.SendCommand($"{PingReplyCommand} {hostMask.Hostname}");
			}
		}
	}
}