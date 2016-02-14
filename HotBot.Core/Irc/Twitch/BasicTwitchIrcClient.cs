using HotBot.Core.Util;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc.Twitch
{
	public sealed class BasicTwitchIrcClient : TwitchIrcClient
	{
		//TODO: [Dependency]
		public TwitchApi ApiProvider { get; set; }
		public IrcConnection Connection { get; private set; }
		public IReadOnlyCollection<Channel> JoinedChannels { get; private set; }
		public string[] MessageOfTheDay { get; private set; }
		public string[] RegisteredCapabilities { get; private set; }
		public string[] SupportedFeatures { get; private set; }
		public MessageBus Bus { get; }

		public BasicTwitchIrcClient(MessageBus bus, IrcConnection connection)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			Bus = bus;
			Bus.Subscribe(this);
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

		public void Login(LoginMethod loginMethod)
		{
			loginMethod.Login(Connection);
		}

		public void Logout(string reason)
		{
			if (string.IsNullOrEmpty(reason))
			{
				Connection.SendCommand("QUIT");
			}
			else
			{
				Connection.SendCommand($"QUIT :{reason}");
			}
		}

		public void SayThirdPerson(Channel channel, string message)
		{
			Say(channel, $"/me {message}");
		}

		public void Say(Channel channel, string message)
		{
			Connection.SendCommand($"PRIVMSG {channel.ToString()} :{message}");
		}

		public void SetDisplayColor(Channel channel, TwitchColor color)
		{
			Say(channel, $"/color {color.ToString()}");
		}

		public void WhisperUser(User target, string message)
		{
			Say(JoinedChannels.First(), $"/w {target.Name} {message}");
		}

		public WhisperConnection GetWhisperConnection(Channel channel)
		{
			throw new NotImplementedException();
		}
	}
}