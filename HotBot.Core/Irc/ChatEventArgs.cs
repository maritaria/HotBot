using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ChatEventArgs : EventArgs
	{
		public Channel Channel { get; }
		public User Sender { get; }
		public string Message { get; }

		public ChatEventArgs(Channel channel, User sender, string message)
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
			Channel = channel;
			Sender = sender;
			Message = message;
		}
	}
}