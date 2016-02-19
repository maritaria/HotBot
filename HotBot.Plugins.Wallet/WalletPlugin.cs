using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using HotBot.Plugins.Wallet;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: AssemblyPlugin(typeof(WalletPlugin))]

namespace HotBot.Plugins.Wallet
{
	public class WalletPlugin : Plugin
	{
		private WalletDatabase _database = new WalletDatabase();

		public PluginDescription Description { get; } = new PluginDescription("Wallet", "Keeps track of players cash and coins.");

		[Dependency]
		public PluginManager PluginManager { get; }

		[Dependency]
		public MessageBus Bus { get; }

		public void Load()
		{
			Bus.Subscribe(this);
			_database.Database.Initialize(true);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
			_database.SaveChanges();
		}
		
		public Wallet GetWallet(User user)
		{
			var wallet = _database.Set<Wallet>().FirstOrDefault(w => w.OwnerId == user.Id);
			if (wallet == null)
			{
				wallet = CreateWallet(user);
			}
			return wallet;
		}

		private Wallet CreateWallet(User user)
		{
			return _database.Set<Wallet>().Create();
		}
	}
}
