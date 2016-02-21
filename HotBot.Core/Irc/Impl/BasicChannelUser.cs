using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannelUser : ChannelUser
	{
		public ChannelData Channel { get; }

		public User GlobalUser { get; }

		public BasicChannelUser(ChannelData channel, User user)
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
			GlobalUser = user;
		}
	}
}