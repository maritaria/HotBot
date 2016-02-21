using HotBot.Core.Intercom;
using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicWhisperConnection : WhisperConnection
	{
		public IrcConnection Connection { get; }
		public TwitchConnector Connector { get; }
		public MessageBus Bus { get; }

		public BasicWhisperConnection(TwitchConnector connector, IrcConnection connection, MessageBus bus)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("connector");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Connector = connector;
			Connection = connection;
			Connection.ResponseReceived += Connection_ResponseReceived;
			Bus = bus;
		}

		public event EventHandler<WhisperEventArgs> WhisperReceived;

		public void SendWhisper(User user, string message)
		{
		}

		private void Connection_ResponseReceived(object sender, ResponseEventArgs e)
		{
			if (e.Response.Command == "WHISPER")
			{
				var user = new BasicUser(e.Response.Arguments[0]);
				var message = e.Response.Arguments[1];
				WhisperReceived?.Invoke(this, new WhisperEventArgs(user, message));
			}
		}
	}
}