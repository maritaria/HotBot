using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Irc.Twitch;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
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
		[Dependency]
		public MessageBus Bus { get; set; }

		[Dependency]
		public MasterConfig MasterConfig { get; set; }

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public TwitchIrcClient TwitchClient { get; set; }

		[Dependency]
		public DataStore DataStore { get; set; }

		public Channel PrimaryChannel { get; } = new Channel("maritaria");

		public TwitchBot()
		{
		}

		public void Run()
		{
			JoinPrimaryChannel();
			DataStore.Initialize();
			Bus.Subscribe(this);
			PluginManager.LoadAll();
		}

		private void JoinPrimaryChannel()
		{
			/*
			Bus.PublishSpecific(new ChannelJoinRequest(PrimaryChannel));
			Bus.PublishSpecific(new ChannelNotificationRequest(PrimaryChannel, "is now online"));
			Bus.PublishSpecific(new RegisterCapabilityRequest(RegisterCapabilityRequest.TwitchMembership));
			Bus.PublishSpecific(new RegisterCapabilityRequest(RegisterCapabilityRequest.ExtendedCommands));
			Bus.PublishSpecific(new ChangeColorRequest(PrimaryChannel, ChangeColorRequest.ChatColor.DodgerBlue));
			*/
			TwitchClient.Connection.Connect("tmi.twitch.tv", 6667);
			var login = new TwitchLogin { AuthKey = "oauth:to4julsv3nu1c6lx9l1s13s7nj25yp", Username = "maritaria_bot01" };
			TwitchClient.Login(login);
			
			while (true)
			{
				Response r = TwitchClient.Connection.ReadResponse();
				Console.WriteLine(r.ToString());
			}
		}
	}
}