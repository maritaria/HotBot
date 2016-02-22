using HotBot.Core.Intercom;
using System;
using System.Collections.Generic;

namespace HotBot.Core.Irc
{
	public interface Channel : Publisher
	{
		IReadOnlyCollection<User> ActiveUsers { get; }
		IrcConnection Connection { get; }
		TwitchConnector Connector { get; }
		string Name { get; }

		void Join();

		void Leave();

		void Say(string message);

		void Broadcast(string message);

		event EventHandler<ChatEventArgs> ChatReceived;

		event EventHandler<UserChannelEventArgs> UserJoined;

		event EventHandler<UserChannelEventArgs> UserLeft;
	}
}