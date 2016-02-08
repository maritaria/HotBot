using System;
using System.Linq;
using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using System.Configuration;
using HotBot.Core.Plugins;

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
		public PluginManager PluginManager { get; }
		public IrcClient IrcClient { get; }

		public TwitchBot(MessageBus bus, PluginManager pluginManager, IrcClient ircClient)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (pluginManager == null)
			{
				throw new ArgumentNullException("pluginManager");
			}
			if (ircClient == null)
			{
				throw new ArgumentNullException("ircClient");
			}
			Bus = bus;
			PluginManager = pluginManager;
			IrcClient = ircClient;
			JoinPrimaryChannel();
			PluginManager.LoadAll();
		}
		
		private void JoinPrimaryChannel()
		{
			Bus.Publish(new ChannelJoinRequest(PrimaryChannel));
			Bus.Publish(new ChannelNotificationRequest(PrimaryChannel, "is now online"));
		}
	}
}