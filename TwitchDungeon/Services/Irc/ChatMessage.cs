using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Irc
{
	public class ChatMessage
	{
		public IrcClient Connection { get; }
		public string Channel { get; }
		public User User { get; }
		public string Message { get; }

		public ChatMessage(IrcClient connection, string channel, User user, string message)
		{
			Connection = connection;
			Channel = channel;
			User = user;
			Message = message;
		}
	}
}
