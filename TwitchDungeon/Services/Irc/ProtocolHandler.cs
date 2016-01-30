using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDungeon.Services.Messages;

namespace TwitchDungeon.Services.Irc
{
	public class ProtocolHandler
	{

		public ProtocolHandler(MessageBus bus)
		{
			bus.Subscribe<IrcMessageReceived>(OnIrcMessage);
		}

		private void OnIrcMessage(MessageBus bus, IrcMessageReceived message)
		{
			bus.Publish(HandleMessage(message.Text));
		}

		private IrcMessageEnhanced HandleMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentException("cannot be empty or null", "message");
			}
			Console.WriteLine(message);

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
			//TODO: Chain of responsibility
			string[] parts = SplitOnFirstOccurence(message, "PRIVMSG");
			string username = parts[0].Substring(0, usernameLength);
			string chatDetails = parts[1].TrimStart(' ').Substring(1);
			string[] chatParts = SplitOnFirstOccurence(chatDetails, " :");
			string channel = chatParts[0];
			string chatmessage = chatParts[1];

			//Defenetly need the C.O.R. here

			User user = GetUser(username);
			
			return new IrcMessageEnhanced(channel, user, chatmessage);
		}
		
		private User GetUser(string username)
		{
			return null;
		}

		private string[] SplitOnFirstOccurence(string source, string splitter)
		{
			string[] parts = source.Split(new string[] { splitter }, StringSplitOptions.None);
			string remainder = string.Join(splitter, parts.Skip(1));
			return new string[] { parts[0], remainder };
		}
	}
}
