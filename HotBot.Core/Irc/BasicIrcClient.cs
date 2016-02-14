using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc
{
	public class BasicIrcClient : IrcClient
	{
		public IrcConnection Connection { get; private set; }
		public IReadOnlyCollection<Channel> JoinedChannels { get; private set; }
		public string[] MessageOfTheDay { get; private set; }
		public string[] RegisteredCapabilities { get; private set; }
		public string[] SupportedFeatures { get; private set; }

		public BasicIrcClient(IrcConnection connection)
		{
			Connection = connection;
		}

		public void JoinChannel(Channel channel)
		{
			Connection.SendCommand($"JOIN {channel.ToString()}");
		}

		public void LeaveChannel(Channel channel)
		{
			Connection.SendCommand($"PART {channel.ToString()}");
		}

		public void Login(LoginMethod loginDetails)
		{
		}

		public void Logout(string reason)
		{
			Connection.Disconnect();
			throw new NotImplementedException();
			//TODO: leave and connect on login
			//https://github.com/SirCmpwn/ChatSharp/blob/master/ChatSharp/IrcClient.cs#L203
		}

		public void Say(Channel channel, string message)
		{
			Connection.SendCommand($"PRIVMSG {channel.ToString()} :{message}");
		}
	}
}