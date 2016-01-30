using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Irc
{
	public class IrcMessageEnhanced
	{
		public string Channel { get; }
		public User User { get; }
		public string Message { get; }

		public IrcMessageEnhanced(string channel, User user, string message)
		{
			Channel = channel;
			User = user;
			Message = message;
		}
	}




}
