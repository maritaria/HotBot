using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class IrcLogger
	{
		public IrcLogger(MessageBus bus)
		{
			bus.Subscribe(this);
		}

		[Subscribe]
		public void HandleMessage(IrcReceivedEvent ircMessage)
		{
			Console.WriteLine($"> {ircMessage.Message}");
		}

		[Subscribe]
		public void HandleMessage(IrcTransmitRequest ircMessage)
		{
			Console.WriteLine($"< {ircMessage.IrcCommand}");
		}
	}
}