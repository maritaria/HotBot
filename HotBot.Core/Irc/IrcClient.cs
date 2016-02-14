using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface IrcClient
	{
		IrcConnection Connection { get; }
		IReadOnlyCollection<Channel> JoinedChannels { get; }
		string[] MessageOfTheDay { get; }
		string[] RegisteredCapabilities { get; }
		string[] SupportedFeatures { get; }
		
		void JoinChannel(Channel channel);
		void LeaveChannel(Channel channel);
		void Say(Channel channel, string message);

		void Login(LoginMethod loginMethod);
		void Logout(string reason);

	}
}