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
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
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