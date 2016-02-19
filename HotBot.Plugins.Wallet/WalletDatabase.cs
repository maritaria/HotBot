using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class WalletDatabase : DbContext
	{
		//TODO: Figure out if entity framework not to drop missing tables.

		public DbSet<Wallet> Wallets { get; set; }

		public WalletDatabase()
		{
		}
	}
}