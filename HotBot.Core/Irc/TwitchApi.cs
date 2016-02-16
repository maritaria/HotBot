using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	//TODO: Doesnt belong in this namespace
	public interface TwitchApi
	{
		IEnumerable<ConnectionInfo> GetChatServers(ChannelData channel);

		IEnumerable<ConnectionInfo> GetWhisperServers();

		//User[] GetViewers(Channel channel);
	}
}
