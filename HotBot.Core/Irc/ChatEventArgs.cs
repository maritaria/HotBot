using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ChatEventArgs : EventArgs
	{
		public LiveChannel Channel { get; }
		public User Sender { get; }
		public string Message { get; }

		public ChatEventArgs(LiveChannel channel, User sender, string message)
		{
			Verify.NotNull(channel, "channel");
			Verify.NotNull(sender, "sender");
			Verify.NotNull(message, "message");
			Channel = channel;
			Sender = sender;
			Message = message;
		}
	}
}