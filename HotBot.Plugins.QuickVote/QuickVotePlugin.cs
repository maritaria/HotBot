using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using System;
using System.Linq;

namespace HotBot.Plugins.QuickVote
{
	public sealed class QuickVotePlugin : Plugin
	{
		public PluginDescription Description { get; }
		public PluginManager Manager { get; }
		public MessageBus Bus { get; }

		public QuickVotePlugin(PluginManager pluginManager, MessageBus bus)
		{
			if (pluginManager == null)
			{
				throw new ArgumentNullException("pluginManager");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Manager = pluginManager;
			Bus = bus;
			Description = new PluginDescription("QuickVote", "Keeps track of recent trends in chat messages");
		}

		public void Load()
		{
			Bus.Subscribe(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
		}

		[Subscribe]
		public void OnChatMessage(ChatReceivedEvent receivedChat)
		{
			//TODO: Keep track of data
		}
	}
}