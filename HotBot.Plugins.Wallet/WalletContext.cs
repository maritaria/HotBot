using System;
using System.Data.Entity;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class WalletContext : DbContext
	{
		public DbSet<WalletValue> WalletValues { get; set; }
	}
}