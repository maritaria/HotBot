using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public sealed class IrcProtocolDecoder : MessageHandler<IrcReceivedEvent>
	{
		public const char IrcMessageStart = ':';

		public MessageBus Bus { get; }

		public IrcProtocolDecoder(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
		}


		public void HandleIrcMessage(string ircMessage)
		{
			//:USERNAME!USERNAME@USERNAME.tmi.twitch.tv PRIVMSG #CHANNEL :MESSAGE
			
			//:NICKNAME!USERNAME@SERVER CODE BODY



		}

		void MessageHandler<IrcReceivedEvent>.HandleMessage(IrcReceivedEvent message)
		{
			HandleIrcMessage(message.Message);
		}
	}
}
