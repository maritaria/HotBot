using HotBot.Core.Unity;
using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	[RegisterFor(typeof(ProtocolDecoder))]
	public sealed class BasicProtocolDecoder : ProtocolDecoder
	{
		public const string PingCommand = "PING";
		public const string ChatCommand = "PRIVMSG";
		public const string JoinCommand = "JOIN";
		public const string LeaveCommand = "PART";

		public TwitchConnector Connector { get; }

		public BasicProtocolDecoder(TwitchConnector connector)
		{
			Verify.NotNull(connector, "connector");
			Connector = connector;
		}

		public event EventHandler<ChatEventArgs> ChatReceived;

		public event EventHandler<PingEventArgs> PingReceived;

		public event EventHandler<UserChannelEventArgs> UserJoinedChannel;

		public event EventHandler<UserChannelEventArgs> UserLefthannel;

		public void Decode(IrcConnection connection, Response response)
		{
			Verify.NotNull(connection, "connection");
			Verify.NotNull(response, "response");
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
			string username = response.Arguments[1];
			string message = response.Arguments[2];
			Channel channel = GetChannel(channelName);
			var user = GetUser(username);
			ChatReceived?.Invoke(this, new ChatEventArgs(channel, user, message));
		}

		private void HandleJoinCommand(IrcConnection connection, Response response)
		{
			string channelName = response.Arguments[0];
			string username = response.Arguments[1];
			string message = response.Arguments[2];
			Channel channel = GetChannel(channelName);
			var user = GetUser(username);
			ChatReceived?.Invoke(this, new ChatEventArgs(channel, user, message));
		}

		private void HandleLeaveCommand(IrcConnection connection, Response response)
		{
			string channelName = response.Arguments[0];
			string username = response.HostMask.Username;
			string message = response.Arguments[2];
			Channel channel = GetChannel(channelName);
			var user = GetUser(username);
			ChatReceived?.Invoke(this, new ChatEventArgs(channel, user, message));
		}

		private Channel GetChannel(string channelName)
		{
			return Connector.ConnectedChannels.First(c => c.Name == channelName);
		}

		private BasicUser GetUser(string username)
		{
			return new BasicUser(username);
		}
	}
}