using HotBot.Core.Irc;
using HotBot.Core.Util;
using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class WalletContext : DbContext
	{
		public DbSet<UserWallet> Wallets { get; set; }

		public IQueryable<UserWallet> GetUserWallets(User user)
		{
			Verify.NotNull(user, "user");
			return Wallets.Where(v => v.UserId == user.Id);
		}

		public UserWallet GetWalletValue(User user, string currency)
		{
			Verify.NotNull(user, "user");
			Verify.Currency(currency, "currency");
			return GetUserWallets(user).FirstOrDefault(v => v.Currency == currency);
		}

		public UserWallet GetOrCreateWalletValue(User user, string currency)
		{
			return GetWalletValue(user, currency) ?? CreateWalletValue(user, currency);
		}

		private UserWallet CreateWalletValue(User user, string currency)
		{
			var wallet = new UserWallet(user, currency);
			Wallets.Add(wallet);
			return wallet;
		}

		internal IQueryable<UserWallet> GetWallets(string currency)
		{
			Verify.Currency(currency, "currency");
			return Wallets.Where(v => v.Currency == currency);

		}
	}
}