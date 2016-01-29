using System;
using TwitchDungeon.Model;
using System.Linq;

namespace TwitchDungeon
{
	//http://tmi.twitch.tv/group/user/maritaria/chatters
	public class TwitchBot
	{
		public static readonly string Hostname = "irc.twitch.tv";
		public static readonly int Port = 6667;

		private object _consoleLock = new object();
		public IrcClient IrcClient { get; }
		public TwitchContext Database { get; private set; }

		public string PrimaryChannel { get; }

		public TwitchBot(string primaryChannel)
		{
			PrimaryChannel = primaryChannel;
			IrcClient = new IrcClient(Hostname, 6667);
			IrcClient.ChatMessageReceived += IrcClient_ChatMessageReceived;
			IrcClient.Connect();
			InitializeDatabaseConnection();
			WriterMethod();
		}

		private void InitializeDatabaseConnection()
		{
			Database = new TwitchContext();
		}

		private void IrcClient_ChatMessageReceived(object sender, IrcChatMessageEventArgs e)
		{
			User user = Database.Users.Where(u => u.Username == e.Username).FirstOrDefault();
			if (user == null)
			{
				user = new User(e.Username);
				user.Money = 100;
				IrcClient.SendMessage(PrimaryChannel, "I have created an account for you :D Enjoy the money (+100)");
				Database.Users.Add(user);
			}
			else
			{
				user.Money += 100;
				IrcClient.SendMessage(PrimaryChannel, $"You deserve a raise ({user.Money})");
			}
			Database.SaveChanges();
		}

		private void WriterMethod()
		{
			IrcClient.Login("maritaria_bot01", "oauth:to4julsv3nu1c6lx9l1s13s7nj25yp");
			IrcClient.JoinChannel(PrimaryChannel);
			IrcClient.SendMessage(PrimaryChannel, "Hello World! I'm a bot :)");
		}
	}
}