using HotBot.Core;
using HotBot.Core.Intercom;
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

[assembly: Plugin(typeof(WalletPlugin))]

namespace HotBot.Plugins.Wallet
{
	public class WalletPlugin : Plugin
	{
		public const string DefaultCurrency = "money";

		public string DefaultCurrencyName { get; set; } = DefaultCurrency;

		public PluginDescription Description { get; } = new PluginDescription("Wallet", "Keeps track of players cash and coins.");

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		public void Load()
		{
			Bus.Subscribe(this);
		}

		public void Unload()
		{
			Bus.Unsubscribe(this);
		}
		
		public double GetCurrency(User walletOwner, string currency)
		{
			if (walletOwner == null)
			{
				throw new ArgumentNullException("walletOwner");
			}
			if (string.IsNullOrEmpty("currency"))
			{
				throw new ArgumentException("Cannot be null or empty", "currency");
			}
			using (var context = new WalletContext())
			{
				var wallet = context.WalletValues.FirstOrDefault(w => w.OwnerId == walletOwner.Id);
				if (wallet == null)
				{
					return 0;
				}
				return wallet.Value;
			}
		}
		
		public void SetCurrency(User walletOwner, string currency, double value)
		{
			using (var context = new WalletContext())
			{
				var wallet = context.WalletValues.FirstOrDefault(w => w.OwnerId == walletOwner.Id);
				if (wallet == null)
				{
					wallet = new WalletValue(walletOwner, currency);
					context.WalletValues.Add(wallet);
				}
				wallet.Value += value;
				context.SaveChanges();
			}
		}
	}
}
