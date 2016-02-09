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
			Bus.Subscribe(this);
		}


		public void HandleIrcMessage(string ircMessage)
		{
			//:USERNAME!USERNAME@USERNAME.tmi.twitch.tv PRIVMSG #CHANNEL :MESSAGE

			//:NICKNAME!USERNAME@SERVER CODE BODY

			//TODO: Implement Irc support

			//WHISPER 
			//PRIVMSG

			//ircMessage.Split

			//https://github.com/justintv/Twitch-API/blob/master/IRC.md

			string[] parts = ircMessage.SplitOnce(" ");
			HostMask hostmask = new HostMask(parts[0]);
			string remainingText = parts[1];

			parts = remainingText.SplitOnce(" ");
			string command = parts[0];
			string args = parts[1];
		}

		void MessageHandler<IrcReceivedEvent>.HandleMessage(IrcReceivedEvent message)
		{
			HandleIrcMessage(message.Message);
		}
	}
}
