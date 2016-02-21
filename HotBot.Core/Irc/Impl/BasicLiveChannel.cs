using HotBot.Core.Intercom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicLiveChannel : LiveChannel
	{
		private List<ChannelUser> _activeUsers = new List<ChannelUser>();

		public ChannelData Data { get; }
		public IrcConnection Connection { get; }
		public TwitchConnector Connector { get; }
		public MessageBus Bus { get; }

		public IReadOnlyCollection<ChannelUser> ActiveUsers { get; }
		
		public BasicLiveChannel(TwitchConnector connector, IrcConnection connection, ChannelData data, MessageBus bus)
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
			ActiveUsers = new ReadOnlyCollection<ChannelUser>(_activeUsers);
			Connector = connector;
			Connector.Decoder.ChatReceived += Decoder_ChatReceived;
			Connection = connection;
			Data = data;
			Bus = bus;
		}

		private void Decoder_ChatReceived(object sender, ChatEventArgs e)
		{
			if (e.Sender.Channel == Data)
			{
				ChatReceived?.Invoke(this, e);
			}
		}

		public event EventHandler<ChatEventArgs> ChatReceived;

		public event EventHandler<ChannelUserEventArgs> UserJoined;

		public event EventHandler<ChannelUserEventArgs> UserLeft;

		public void Join()
		{
			Connection.SendCommand($"JOIN #{Data.Name}");
		}

		public void Leave()
		{
			Connection.SendCommand($"PART #{Data.Name}");
		}

		public void Say(string message)
		{
			Connection.SendCommand($"PRIVMSG #{Data.Name} :{message}");
		}
	}
}