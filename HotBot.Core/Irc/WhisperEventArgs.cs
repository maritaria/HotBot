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
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			Sender = sender;
			Message = message;
		}
	}
}