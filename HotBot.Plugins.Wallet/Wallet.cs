using HotBot.Core.Irc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HotBot.Plugins.Wallet
{
	//TODO: Global wallets vs channel wallets
	public class Wallet
	{
		public Guid OwnerId { get; private set; }

		//TODO: How to init this? should i use a diff type that the abstraction will populate for us?
		public DbSet<WalletValue> Values { get; private set; }
		
		protected Wallet()
		{

		}
		
		public Wallet(User owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			OwnerId = owner.Id;
		}

		private WalletValue GetCurrency(string currency)
		{
			return Values.First(w=>w.Currency.ToLower() == currency.ToLower());
		}
	}
}