namespace HotBot.Core.Irc
{
	public interface ChannelConnection
	{
		Channel ChannelData { get; }
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }

		void Join();
		void Leave();
		void Say(string message);
	}

}