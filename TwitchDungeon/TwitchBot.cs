using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using TwitchDungeon.DataStorage;

namespace TwitchDungeon
{
	//http://tmi.twitch.tv/group/user/maritaria/chatters
	public class TwitchBot
	{
		public static readonly string Hostname = "irc.twitch.tv";
		public static readonly int Port = 6667;

		private object _consoleLock = new object();
		public IrcClient IrcClient { get; }

		public string PrimaryChannel { get; }
		
		public TwitchBot(string primaryChannel)
		{
			PrimaryChannel = primaryChannel;
			IrcClient = new IrcClient(Hostname, 6667);
			IrcClient.ChatMessageReceived += IrcClient_ChatMessageReceived;
			IrcClient.Connect();
			WriterMethod();
		}

		private void IrcClient_ChatMessageReceived(object sender, IrcChatMessageEventArgs e)
		{
			IrcClient.SendMessage(PrimaryChannel, "Hello World!");

			User user = new User(e.Username);
			user.Money = 100;

			RepositoryHelper.Users.Save(user);

		}

		private void WriterMethod()
		{
			IrcClient.Login("maritaria_bot01", "oauth:to4julsv3nu1c6lx9l1s13s7nj25yp");
			IrcClient.JoinChannel(PrimaryChannel);
			IrcClient.SendMessage(PrimaryChannel, "Hello World! I'm a bot :)");
		}
	}
}
