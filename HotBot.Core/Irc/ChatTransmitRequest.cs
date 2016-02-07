using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class ChatTransmitRequest : IrcTransmitRequest
	{
		public const string IrcMessageCommand = "PRIVMSG";
		private string _ircCommand;
		private string _text;

		public string Text
		{
			get { return _text; }
			protected set
			{
				_text = value;
				_ircCommand = GenerateIrcCommand(value);
			}
		}

		public Channel Channel { get; }
		public override sealed string IrcCommand => _ircCommand;

		protected ChatTransmitRequest(Channel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			Channel = channel;
		}

		public ChatTransmitRequest(Channel channel, string chatMessage) : this(channel)
		{
			if (chatMessage == null)
			{
				throw new ArgumentNullException("chatMessage");
			}
			Text = chatMessage;
		}

		protected virtual string GenerateIrcCommand(string text)
		{
			return $"{IrcMessageCommand} {Channel.ToString()} :{text}";
		}
	}
}