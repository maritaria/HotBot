using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannelConnection : ChannelConnection
	{
		public Channel ChannelData { get; }
		public IrcConnection Connection { get; }
		public TwitchConnector Connector { get; }
		public MessageBus Bus { get; }

		public BasicChannelConnection(TwitchConnector connector, IrcConnection connection, Channel data, MessageBus bus)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("connector");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Connector = connector;
			Connection = connection;
			ChannelData = data;
			Bus = bus;
		}

		public event EventHandler<ChatEventArgs> ChatReceived;

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