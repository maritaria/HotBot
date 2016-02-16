using HotBot.Core;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Plugins.Wallet
{
	public class WalletPlugin : Plugin
	{
		public PluginDescription Description { get; } = new PluginDescription("Wallet", "Keeps track of players cash and coins.");

		[Dependency]
		public PluginManager PluginManager { get; }

		[Dependency]
		public MessageBus Bus { get; }

		public void Load()
		{
			Bus.Subscribe(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
		}
	}
}
