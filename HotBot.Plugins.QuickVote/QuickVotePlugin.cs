using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using HotBot.Plugins.QuickVote;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

[assembly: AssemblyPlugin(typeof(QuickVotePlugin))]

namespace HotBot.Plugins.QuickVote
{
	public sealed class QuickVotePlugin : Plugin
	{
		public PluginDescription Description { get; } = new PluginDescription("QuickVote", "Keeps track of recent trends in chat messages");

		[Dependency]
		public MessageBus Bus { get; set; }

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public CommandManager CommandManager { get; set; }

		public void Load()
		{
			Bus.Subscribe(this);
			CommandManager.Register(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
			CommandManager.Unregister(this);
		}

		[Subscribe]
		public void OnChatMessage(/*ChatReceivedEvent receivedChat*/)
		{
			//TODO: Keep track of data
		}
	}
}