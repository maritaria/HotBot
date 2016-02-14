using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannelConnection : ChannelConnection
	{
		public Channel ChannelData { get; private set; }

		public IrcConnection Connection { get; private set; }

		public TwitchConnector Connector { get; private set; }

		public BasicChannelConnection(TwitchConnector connector, IrcConnection connection, Channel data)
		{
			Connector = connector;
			Connection = connection;
			ChannelData = data;
		}

		public void Join()
		{
			Connection.SendCommand($"JOIN #{ChannelData.Name}");
		}

		public void Leave()
		{
			Connection.SendCommand($"PART #{ChannelData.Name}");
		}

		public void Say(string message)
		{
			Connection.SendCommand($"PRIVMSG #{ChannelData.Name} :{message}");
		}
	}
}