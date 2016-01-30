using System;
using System.Linq;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Messages;
using TwitchDungeon.Services.Util;

namespace TwitchDungeon.Services.Irc
{
	public class ProtocolHandler
	{
		public DataStore Database { get; }

		public ProtocolHandler(MessageBus bus, DataStore database)
		{
			Database = database;
			bus.Subscribe<IrcMessageReceived>(OnIrcMessageReceived);
		}

		private void OnIrcMessageReceived(MessageBus bus, IrcMessageReceived message)
		{
			IrcMessageEnhanced enhancedMessage = HandleMessage(message.Text);

			if (enhancedMessage != null)
			{
				bus.Publish(enhancedMessage);
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
			Channel channel = Database.Channels.FirstOrDefault(c => c.Name == channelName);

			return new IrcMessageEnhanced(channel, user, chatmessage);
		}

		private User GetUser(string username)
		{
			return null;
		}
	}
}