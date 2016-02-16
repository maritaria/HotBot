using System;
using System.Linq;

namespace HotBot.Core.Util
{
	public static class Verify
	{
		public const int MinimumUsernameLength = 4;
		public const int MaximumUsernameLength = 25;
		public const int MinimumChannelNameLength = MinimumUsernameLength;
		public const int MaximumChannelNameLength = MaximumUsernameLength;

		public static void Username(string username)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.Length < MinimumUsernameLength)
			{
				throw new ArgumentException($"A username must be at least {MinimumUsernameLength} characters", "username");
			}
			if (username.Length > MaximumUsernameLength)
			{
				throw new ArgumentException($"A username cannot be longer than {MaximumUsernameLength} characters", "username");
			}
		}

		public static void ChannelName(string channelName)
		{
			if (channelName == null)
			{
				throw new ArgumentNullException("channelName");
			}
			if (channelName.Length < MinimumUsernameLength)
			{
				throw new ArgumentException($"A channel name must be at least {MinimumUsernameLength} characters", "channelName");
			}
			if (channelName.Length > MaximumUsernameLength)
			{
				throw new ArgumentException($"A channel name cannot be longer than {MaximumUsernameLength} characters", "channelName");
			}
		}
	}
}