using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicProtocolDecoder : ProtocolDecoder
	{
		public const string PingCommand = "PING";
		public const string ChatCommand = "PRIVMSG";
		public const string JoinCommand = "JOIN";
		public const string LeaveCommand = "PART";

		public TwitchConnector Connector { get; }

		public BasicProtocolDecoder(TwitchConnector connector)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("connector");
			}
			Connector = connector;
		}

		public event EventHandler<ChatEventArgs> ChatReceived;

		public event EventHandler<PingEventArgs> PingReceived;

		public void Decode(IrcConnection connection, Response response)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			switch (response.Command)
			{
				case PingCommand:
					HandlePingRequest(connection, response);
					break;

				case ChatCommand:
					HandleChatMessage(connection, response);
					break;

				case JoinCommand:
					HandleJoinCommand(connection, response);
					break;

				case LeaveCommand:
					HandleLeaveCommand(connection, response);
					break;
			}
		}

		private void HandlePingRequest(IrcConnection connection, Response response)
		{
			PingReceived?.Invoke(this, new PingEventArgs(connection));
		}

		private void HandleChatMessage(IrcConnection connection, Response response)
		{
			string channelName = response.Arguments[0];
			Channel channel = Connector.ConnectedChannels.First(c => c.ChannelData.Name == channelName).ChannelData;
			string username = response.Arguments[1];
			ChannelUser user = new BasicChannelUser();
			string message = response.Arguments[2];
			ChatReceived?.Invoke(this, new ChatEventArgs(user, message));
		}

		private void HandleJoinCommand(IrcConnection connection, Response response)
		{
			throw new NotImplementedException();
		}

		private void HandleLeaveCommand(IrcConnection connection, Response response)
		{
			throw new NotImplementedException();
		}
	}
}