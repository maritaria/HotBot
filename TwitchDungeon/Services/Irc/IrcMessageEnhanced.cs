using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDungeon.Services.DataStorage;

namespace TwitchDungeon.Services.Irc
{
	public class IrcMessageEnhanced
	{
		public Channel Channel { get; }
		public User User { get; }
		public string Text { get; }

		public IrcMessageEnhanced(Channel channel, User user, string message)
		{
			Channel = channel;
			User = user;
			Text = message;
		}
	}




}
