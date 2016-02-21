using HotBot.Core.Util;
using System;

namespace HotBot.Core.Irc
{
	public sealed class UserChannelEventArgs : EventArgs
	{
		public User User { get; }
		public Channel Channel { get; }

		public UserChannelEventArgs(User user, Channel channel)
		{
			Verify.NotNull(user, "user");
			Verify.NotNull(channel, "channel");
			User = user;
			Channel = channel;
		}
	}
}