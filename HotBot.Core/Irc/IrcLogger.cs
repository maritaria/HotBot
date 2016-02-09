using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class IrcLogger : MessageHandler<IrcReceivedEvent>, MessageHandler<IrcTransmitRequest>
	{
		public IrcLogger(MessageBus bus)
		{
			bus.Subscribe<IrcReceivedEvent>(this);
			bus.Subscribe<IrcTransmitRequest>(this);
		}

		public void HandleMessage(IrcReceivedEvent ircMessage)
		{
			Console.WriteLine($"> {ircMessage.Message}");
		}

		public void HandleMessage(IrcTransmitRequest ircMessage)
		{
			Console.WriteLine($"< {ircMessage.IrcCommand}");
		}
	}
}