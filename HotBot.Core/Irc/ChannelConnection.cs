using System;

namespace HotBot.Core.Irc
{
	public interface ChannelConnection : Publisher
	{
		Channel ChannelData { get; }
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }

		void Join();
		void Leave();
		void Say(string message);

		event EventHandler<ChatEventArgs> ChatReceived;

	}
}