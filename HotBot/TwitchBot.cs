using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using System;
using System.Linq;

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

		public TwitchBot(MessageBus bus, PluginManager pluginManager, IrcClient ircClient, DataStore dataStore)
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
			if (dataStore == null)
			{
				throw new ArgumentNullException("dataStore");
			}
			Bus = bus;
			PluginManager = pluginManager;
			IrcClient = ircClient;
			JoinPrimaryChannel();
			PluginManager.LoadAll();
			dataStore.Initialize();
		}

		private void JoinPrimaryChannel()
		{
			Bus.PublishSpecific(new ChannelJoinRequest(PrimaryChannel));
			Bus.PublishSpecific(new ChannelNotificationRequest(PrimaryChannel, "is now online"));
			Bus.PublishSpecific(new RegisterCapabilityRequest(RegisterCapabilityRequest.TwitchMembership));
			Bus.PublishSpecific(new RegisterCapabilityRequest(RegisterCapabilityRequest.ExtendedCommands));
			Bus.PublishSpecific(new ChangeColorRequest(PrimaryChannel, ChangeColorRequest.ChatColor.DodgerBlue));
		}
	}
}