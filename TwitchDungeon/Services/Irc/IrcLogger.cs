using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDungeon.Services.Messages;

namespace TwitchDungeon.Services.Irc
{
	public class IrcLogger
	{
		public IrcLogger(MessageBus bus)
		{
			bus.Subscribe<IrcMessageReceived>(OnIrcMessageReceived);
		}

		private void OnIrcMessageReceived(MessageBus bus, IrcMessageReceived ircMessage)
		{
			Console.WriteLine(ircMessage.Text);
		}
	}
}
