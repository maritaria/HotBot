using System;
using System.Linq;
using HotBot.Core.DataStorage;
using HotBot.Core.Util;

namespace HotBot.Core.Irc
{
	public class PrivateMessageDecoder : MessageHandler<IrcMessageReceived>
	{
		public DataStore DataStore { get; }
		public MessageBus Bus { get; }

		public PrivateMessageDecoder(DataStore datastore, MessageBus bus)
		{
			if (datastore == null)
			{
				throw new ArgumentNullException("datastore");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			DataStore = datastore;
			Bus = bus;
			bus.Subscribe(this);
		}

		public void HandleMessage(IrcMessageReceived message)
		{
			IrcMessageEnhanced enhancedMessage = HandleMessage(message.Message);

			if (enhancedMessage != null)
			{
				Bus.Publish(enhancedMessage);
			}
		}

		private IrcMessageEnhanced HandleMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentException("cannot be empty or null", "message");
			}

			if (!message.Contains("PRIVMSG"))
			{
				return null;
			}
			//:USERNAME!USERNAME@USERNAME.tmi.twitch.tv PRIVMSG #CHANNEL :MESSAGE
			message = message.Substring(1);
			int usernameLength = 0;
			for (int i = 0; i < message.Length; i++)
			{
				char c = message[i];
				if (c == '!')
				{
					usernameLength = i;
					break;
				}
			}
			//TODO: Clean up this code
			string[] parts = message.SplitOnce("PRIVMSG");
			string username = parts[0].Substring(0, usernameLength);
			string chatDetails = parts[1].TrimStart(' ').Substring(1);
			string[] chatParts = chatDetails.SplitOnce(" :");
			string channelName = chatParts[0];
			string chatmessage = chatParts[1];

			User user = GetUser(username);
			Channel channel = GetChannel(channelName);

			return new IrcMessageEnhanced(channel, user, chatmessage);
		}

		private User GetUser(string username)
		{
			User user = DataStore.Users.FirstOrDefault(u => u.Name == username);
			if (user == null)
			{
				user = new User(username);//TODO: Add new user message
				DataStore.Users.Add(user);
				DataStore.SaveChanges();
			}
			return user;
		}

		private Channel GetChannel(string channelName)
		{
			return DataStore.Channels.FirstOrDefault(c => c.Name == channelName);
		}
	}
}