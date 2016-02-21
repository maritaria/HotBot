using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class WhisperEventArgs : EventArgs
	{
		public User Sender { get; }
		public string Message { get; }

		public WhisperEventArgs(User sender, string message)
		{
			Verify.NotNull(sender, "sender");
			Verify.NotNull(message, "message");
			Sender = sender;
			Message = message;
		}
	}
}