using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public sealed class ChatEventArgs : EventArgs
	{
		public LiveChannel Channel { get; }
		public ChannelUser Sender { get; }
		public string Message { get; }

		public ChatEventArgs(LiveChannel channel, ChannelUser sender, string message)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (channel.Data != sender.Channel)
			{
				throw new ArgumentException("ChannelUser doesn't belong to the specified ChannelConnection");
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