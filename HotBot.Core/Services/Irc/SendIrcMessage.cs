using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Services.Irc
{
	public class SendIrcMessage
	{
		public virtual string IrcCommand { get; }
		
		public SendIrcMessage(string ircCommand)
		{
			IrcCommand = ircCommand;
		}

	}
}
