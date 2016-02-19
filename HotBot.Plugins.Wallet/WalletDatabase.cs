using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class WalletDatabase : DbContext
	{
		public DbSet<Wallet> Wallets { get; set; }

		public WalletDatabase()
		{
		}
	}
}