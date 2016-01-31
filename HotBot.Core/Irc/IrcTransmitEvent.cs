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

		protected IrcTransmitEvent()
		{

		}

		public IrcTransmitEvent(string ircCommand)
		{
			if (ircCommand == null)
			{
				throw new ArgumentNullException("ircCommand");
			}
			IrcCommand = ircCommand;
		}

	}
}
