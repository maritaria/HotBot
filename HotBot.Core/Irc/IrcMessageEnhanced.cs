using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBot.Core;

namespace HotBot.Core.Irc
{
	public class IrcMessageEnhanced
	{
		public Channel Channel { get; }
		public User User { get; }
		public string Message { get; }

		public IrcMessageEnhanced(Channel channel, User user, string message)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			Channel = channel;
			User = user;
			Message = message;
		}
	}




}
