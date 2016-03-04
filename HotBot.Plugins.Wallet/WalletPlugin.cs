using HotBot.Core.Intercom;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using HotBot.Core.Util;
using HotBot.Plugins.Wallet;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

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

		public double GetCurrency(User user, string currency)
		{
			Verify.NotNull(user, "user");
			Verify.Currency(currency, "currency");
			using (var context = new WalletContext())
			{
				var wallet = context.GetWalletValue(user, currency);
				if (wallet == null)
				{
					return 0;
				}
				return wallet.Value;
			}
		}

		public void SetCurrency(User user, string currency, double value)
		{
			Verify.NotNull(user, "user");
			Verify.Currency(currency, "currency");
			using (var context = new WalletContext())
			{
				var wallet = context.GetOrCreateWalletValue(user, currency);
				wallet.Value += value;
				context.SaveChanges();
			}
		}

		public void RemoveCurrency(string currency)
		{
			Verify.Currency(currency, "currency");
			using (var context = new WalletContext())
			{
				IQueryable<UserWallet> wallets = context.GetWallets(currency);
			}
		}
	}
}