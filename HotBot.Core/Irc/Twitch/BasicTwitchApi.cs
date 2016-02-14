using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Twitch
{
	public sealed class BasicTwitchApi : TwitchApi
	{
		public string[] GetGroupChatServers(Channel channel)
		{
			throw new NotImplementedException();
		}

		public User[] GetViewers(Channel channel)
		{
			throw new NotImplementedException();
		}
	}
}
