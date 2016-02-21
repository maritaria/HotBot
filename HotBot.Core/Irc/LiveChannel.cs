using HotBot.Core.Intercom;
using System;
using System.Collections.Generic;

namespace HotBot.Core.Irc
{
	public interface LiveChannel : Publisher
	{
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }
		string Name { get; }

		/// <summary>
		/// Gets the users currently in the <see cref="ChannelData"/>.
		/// </summary>
		IReadOnlyCollection<User> ActiveUsers { get; }

		void Join();

		void Leave();

		void Say(string message);

		event EventHandler<ChatEventArgs> ChatReceived;

		event EventHandler<UserChannelEventArgs> UserJoined;

		event EventHandler<UserChannelEventArgs> UserLeft;
	}
}