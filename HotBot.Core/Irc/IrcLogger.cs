using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public class IrcLogger : MessageHandler<IrcReceivedEvent>
	{
		public IrcLogger(MessageBus bus)
		{
			bus.Subscribe(this);
		}
		
		public void HandleMessage(IrcReceivedEvent ircMessage)
		{
			Console.WriteLine(ircMessage.Message);
		}
	}
}
