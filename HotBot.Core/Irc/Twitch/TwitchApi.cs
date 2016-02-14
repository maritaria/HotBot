using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Twitch
{
	public interface TwitchApi
	{
		//TODO: On twitch; room == channel?
		string[] GetGroupChatServers(Channel channel);

		User[] GetViewers(Channel channel);


	}
}
