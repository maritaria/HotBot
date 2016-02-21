using HotBot.Core.Util;
using System;

namespace HotBot.Core.Irc
{
	public sealed class UserChannelEventArgs : EventArgs
	{
		public User User { get; }
		public LiveChannel Channel { get; }

		public UserChannelEventArgs(User user, LiveChannel channel)
		{
			Verify.NotNull(user, "user");
			Verify.NotNull(channel, "channel");
			User = user;
			Channel = channel;
		}
	}
}