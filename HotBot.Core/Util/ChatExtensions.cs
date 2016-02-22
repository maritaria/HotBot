using HotBot.Core.Irc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Util
{
	public static class ChatExtensions
	{
		public static void Whisper(this User user, Channel channel, string message)
		{
			Verify.NotNull(channel, "channel");
			Whisper(user, channel.Connector, message);
		}

		public static void Whisper(this User user, TwitchConnector connector, string message)
		{
			Verify.NotNull(connector, "connector");
			Whisper(user, connector.WhisperServer, message);
		}

		public static void Whisper(this User user, WhisperConnection whisperConnection, string message)
		{
			Verify.NotNull(user, "user");
			Verify.NotNull(whisperConnection, "whisperConnection");
			Verify.NotNull(message, "message");
			whisperConnection.SendWhisper(user, message);
		}

		public static void Broadcast(IEnumerable<Channel> channels, string message)
		{
			foreach (Channel channel in channels)
			{
				channel.Say(message);
			}
		}

		public static void BroadcastAnnounce(IEnumerable<Channel> channels, string message)
		{
			foreach(Channel channel in channels)
			{
				channel.Announce(message);
			}
		}

		public static void Callout(this User user, Channel channel, string message)
		{
			Verify.NotNull(channel, "channel");
			Verify.NotNull(user, "user");
			Verify.NotNull(message, "message");
			channel.Say($"@{user.Name} {message}");
		}

		public static void Callout(this Channel channel, User user, string message)
		{
			user.Callout(channel, message);
		}

		public static void CalloutByAnnounce(this User user, Channel channel, string message)
		{
			Verify.NotNull(channel, "channel");
			Verify.NotNull(user, "user");
			Verify.NotNull(message, "message");
			channel.Announce($"@{user.Name} {message}");
		}

		public static void CalloutByAnnounce(this Channel channel, User user, string message)
		{
			user.CalloutByAnnounce(channel, message);
		}
	}
}