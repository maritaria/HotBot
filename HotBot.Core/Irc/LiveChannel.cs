using System;
using System.Collections.Generic;

namespace HotBot.Core.Irc
{
	public interface LiveChannel : Publisher
	{
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }
		ChannelData Data { get; }
		/// <summary>
		/// Gets the users currently in the <see cref="ChannelData"/>.
		/// </summary>
		IReadOnlyCollection<ChannelUser> ActiveUsers { get; }

		void Join();
		void Leave();
		void Say(string message);

		event EventHandler<ChatEventArgs> ChatReceived;
		
		event EventHandler<ChannelUserEventArgs> UserJoined;

		event EventHandler<ChannelUserEventArgs> UserLeft;

	}
}