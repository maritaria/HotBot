using System;

namespace HotBot.Core.Irc
{
	public sealed class ChannelUserEventArgs : EventArgs
	{
		public ChannelUser User { get; }

		public ChannelUserEventArgs(ChannelUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			User = user;
		}
	}
}