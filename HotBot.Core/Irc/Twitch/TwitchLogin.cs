using System;
using System.Linq;

namespace HotBot.Core.Irc.Twitch
{
	public sealed class TwitchLogin : LoginMethod
	{
		public string Username { get; set; }
		public string AuthKey { get; set; }

		public void Login(IrcConnection connection)
		{
			DoLogin(connection);
			RequestCapabilities(connection);
		}

		private void DoLogin(IrcConnection connection)
		{
			/*
				< PASS oauth:twitch_oauth_token
				< NICK twitch_username
				> :tmi.twitch.tv 001 twitch_username :connected to TMI
				> :tmi.twitch.tv 002 twitch_username :your host is TMI
				> :tmi.twitch.tv 003 twitch_username :this server is pretty new
				> :tmi.twitch.tv 004 twitch_username tmi.twitch.tv 0.0.1 w n
				> :tmi.twitch.tv 375 twitch_username :- tmi.twitch.tv Message of the day -
				> :tmi.twitch.tv 372 twitch_username :- not much to say here
				> :tmi.twitch.tv 376 twitch_username :End of /MOTD command
			*/
			connection.SendCommandBatch(
				$"PASS {AuthKey}",
				$"USER {Username} * *: {Username}",
				$"NICK {Username}");
		}

		private void RequestCapabilities(IrcConnection connection)
		{
			//twitch.tv/commands
			//twitch.tv/membership
			//twitch.tv/tags

			connection.SendCommand("CAP REQ :twitch.tv/commands");
			connection.SendCommand("CAP REQ :twitch.tv/membership");
			connection.SendCommand("CAP END");

			//CAP REQ :{CAPABILITY}
			//CAP * ACK
			//:tmi.twitch.tv CAP * ACK :twitch.tv/commands
			//CAP END
		}
	}
}