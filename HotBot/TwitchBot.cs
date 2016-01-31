using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;

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

		public TwitchBot(IrcClient ircClient, PipelineInitializer messagePipeline)
		{
			IrcClient = ircClient;
			IrcClient.Connect(Hostname, Port);//TODO: connect inside the IrcClient class using config data
			messagePipeline.Initialize();
			WriterMethod();
		}
		
		private void WriterMethod()
		{
			IrcClient.Login("maritaria_bot01", "oauth:to4julsv3nu1c6lx9l1s13s7nj25yp");
			IrcClient.JoinChannel(PrimaryChannel);
			IrcClient.SendMessage(PrimaryChannel, "Hello World! I'm a bot :)");
			IrcClient.SendMessage(PrimaryChannel, "/mods");
		}
	}
}