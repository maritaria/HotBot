using System;

namespace HotBot.Core.Irc
{
	public interface ProtocolDecoder
	{
		TwitchConnector Connector { get; }

		void Decode(IrcConnection connection, Response response);

		event EventHandler<PingEventArgs> PingReceived;

		event EventHandler<ChatEventArgs> ChatReceived;

		event EventHandler<ChannelUserEventArgs> UserJoinedChannel;

		event EventHandler<ChannelUserEventArgs> UserLefthannel;
	}
}