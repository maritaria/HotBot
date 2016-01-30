using System;

namespace TwitchDungeon.Services.Irc
{
	public sealed class ChatMessageEventArgs : EventArgs
	{
		public ChatMessage Message { get; }

		public ChatMessageEventArgs(ChatMessage message)
		{
			Message = message;
		}
	}
}