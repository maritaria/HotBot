using System;

namespace HotBot.Core.Irc
{
	public sealed class ChannelUserEventArgs : EventArgs
	{
		public Channel Channel { get; }
		public User User { get; }

		public ChannelUserEventArgs(Channel channel, User user)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			Channel = channel;
			User = user;
		}
	}
}