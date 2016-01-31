using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class ChatTransmitEvent : IrcTransmitEvent
	{
		private string _ircCommand;
		public string Text { get; }
		public Channel Channel { get; }
		public override string IrcCommand => _ircCommand;

		public ChatTransmitEvent(Channel channel, string chatMessage)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (chatMessage == null)
			{
				throw new ArgumentNullException("chatMessage");
			}
			Channel = channel;
			Text = chatMessage;
			_ircCommand = GenerateCommand(channel, chatMessage);
		}

		private string GenerateCommand(Channel channel, string chatMessage)
		{
			return $"PRIVMSG {channel.ToString()} :{chatMessage}";
		}
	}
}