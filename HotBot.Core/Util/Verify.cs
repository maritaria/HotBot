using System;
using System.Linq;

namespace HotBot.Core.Util
{
	public static class Verify
	{
		public static void NotNull(object value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		public static void NotEmptyOrNullString(string value, string paramName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Cannot be null or empty", value);
			}
		}

		public static void MinimumLength(string value, int length, string paramName)
		{
			if (value.Length < length)
			{
				throw new ArgumentException($"Must have a length of at least {length}", paramName);
			}
		}

		public static void MaximumLength(string value, int length, string paramName)
		{
			if (value.Length > length)
			{
				throw new ArgumentException($"May have a length of at most {length}", paramName);
			}
		}

		public const int MinimumUsernameLength = 4;
		public const int MaximumUsernameLength = 25;

		public static void Username(string username, string paramName)
		{
			NotNull(username, paramName);
			MinimumLength(username, MinimumUsernameLength, paramName);
			MaximumLength(username, MaximumUsernameLength, paramName);
		}

		public const int MinimumChannelNameLength = MinimumUsernameLength;
		public const int MaximumChannelNameLength = MaximumUsernameLength;

		public static void ChannelName(string channelName, string paramName)
		{
			NotNull(channelName, paramName);
			MinimumLength(channelName, MinimumChannelNameLength, paramName);
			MaximumLength(channelName, MaximumChannelNameLength, paramName);
		}

		public static void CommandName(string commandName, string paramName)
		{
			NotEmptyOrNullString(commandName, paramName);
			if (commandName.Any(char.IsWhiteSpace))
			{
				throw new ArgumentException("cannot contain whitespace(s)", paramName);
			}
		}
	}
}