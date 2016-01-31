using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public class IrcTransmitEvent
	{
		public virtual string IrcCommand { get; }
		
		public IrcTransmitEvent(string ircCommand)
		{
			IrcCommand = ircCommand;
		}

	}
}
