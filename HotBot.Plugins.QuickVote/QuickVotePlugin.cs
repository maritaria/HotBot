using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using HotBot.Plugins.QuickVote;
using System;
using System.Linq;

[assembly: AssemblyPlugin(typeof(QuickVotePlugin))]
namespace HotBot.Plugins.QuickVote
{
	public sealed class QuickVotePlugin : Plugin
	{
		public MessageBus Bus { get; }
		public PluginDescription Description { get; }
		public PluginManager PluginManager { get; }
		public CommandManager CommandManager { get; }

		public QuickVotePlugin(MessageBus bus, PluginManager pluginManager, CommandManager commandManager)
		{
			if (pluginManager == null)
			{
				throw new ArgumentNullException("pluginManager");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (commandManager == null)
			{
				throw new ArgumentNullException("commandManager");
			}
			Bus = bus;
			Description = new PluginDescription("QuickVote", "Keeps track of recent trends in chat messages");
			PluginManager = pluginManager;
			CommandManager = commandManager;
		}

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
		public void OnChatMessage(ChatReceivedEvent receivedChat)
		{
			//TODO: Keep track of data
		}
	}
}