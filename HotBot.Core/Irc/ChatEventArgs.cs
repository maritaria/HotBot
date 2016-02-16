using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ChatEventArgs : EventArgs
	{
		public ChannelUser Sender { get; }
		public string Message { get; }

		public ChatEventArgs(ChannelUser sender, string message)
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