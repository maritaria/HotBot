using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc
{
	//TODO: Doesnt belong in this namespace
	public interface TwitchApi
	{
		IEnumerable<ConnectionInfo> GetChatServers(string channelName);

		IEnumerable<ConnectionInfo> GetWhisperServers();

		//TODO: User[] GetViewers(Channel channel);
	}
}