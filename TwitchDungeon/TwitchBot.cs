using System;
using System.Linq;
using TwitchDungeon.Services;
using TwitchDungeon.Services.Commands;
using TwitchDungeon.Services.Irc;

namespace TwitchDungeon
{
	//http://tmi.twitch.tv/group/user/maritaria/chatters
	public class TwitchBot
	{
		public static readonly string Hostname = "irc.twitch.tv";
		public static readonly int Port = 6667;

		private object _consoleLock = new object();
		public IrcClient IrcClient { get; }
		public CommandHandlerService CommandHandlerService { get; }

		public string PrimaryChannel { get; } = "maritaria";

		public TwitchBot(IrcClient ircClient, PipelineInitializer messagePipeline, CommandPipeline commandPipeline)
		{
			IrcClient = ircClient;// new IrcClient(Hostname, 6667);
			IrcClient.Connect();//TODO: check if connected if not connect
			WriterMethod();
		}
		
		private void WriterMethod()
		{
			IrcClient.Login("maritaria_bot01", "oauth:to4julsv3nu1c6lx9l1s13s7nj25yp");
			IrcClient.JoinChannel(PrimaryChannel);
			IrcClient.SendMessage(PrimaryChannel, "Hello World! I'm a bot :)");
		}
	}
}