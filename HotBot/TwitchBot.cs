using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using System.Configuration;

namespace HotBot
{
	//http://tmi.twitch.tv/group/user/maritaria/chatters
	public class TwitchBot
	{
		public static readonly string Hostname = "irc.twitch.tv";
		public static readonly UInt16 Port = 6667;

		private object _consoleLock = new object();
		public IrcClient IrcClient { get; }

		public string PrimaryChannel { get; } = "maritaria";

		public TwitchBot(IrcClient ircClient)
		{
			IrcClient = ircClient;
			WriterMethod();
		}
		
		private void WriterMethod()
		{
			IrcClient.JoinChannel(PrimaryChannel);
			IrcClient.SendMessage(PrimaryChannel, "Hello World! I'm a bot :)");
			IrcClient.SendMessage(PrimaryChannel, "/mods");
		}
	}
}