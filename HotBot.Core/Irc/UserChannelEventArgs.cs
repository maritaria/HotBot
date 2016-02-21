using System;

namespace HotBot.Core.Irc
{
	public sealed class UserChannelEventArgs : EventArgs
	{
		public User User { get; }
		public LiveChannel Channel { get; }

		public UserChannelEventArgs(User user, LiveChannel channel)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			User = user;
			Channel = channel;
		}
	}
}