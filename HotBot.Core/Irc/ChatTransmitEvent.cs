using System;
using System.Linq;
using HotBot.Core;

namespace HotBot.Core.Irc
{
	public class ChatTransmitEvent : IrcTransmitEvent
	{
		public string Text { get; }
		public Channel Channel { get; }

		public ChatTransmitEvent(Channel channel, string chatMessage) : base(GenerateCommand(channel, chatMessage))
		{
			Text = chatMessage;
			Channel = channel;
		}

		private static string GenerateCommand(Channel channel, string chatMessage)
		{
			return "PRIVMSG #" + channel.Name + " :" + chatMessage;
		}
	}
}