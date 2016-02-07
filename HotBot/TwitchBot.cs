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
		public MessageBus Bus { get; }

		public Channel PrimaryChannel { get; } = new Channel("maritaria");

		public TwitchBot(MessageBus bus)
		{
			Bus = bus;
			JoinPrimaryChannel();
		}
		
		private void JoinPrimaryChannel()
		{
			Bus.Publish(new ChannelJoinRequest(PrimaryChannel));
			Bus.Publish(new ChannelNotificationRequest(PrimaryChannel, "is now online"));
		}
	}
}