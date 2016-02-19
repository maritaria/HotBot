using HotBot.Core.Irc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HotBot.Plugins.Wallet
{
	//TODO: Global wallets vs channel wallets
	public sealed class Wallet : IQueryable<WalletValue>
	{
		public User Owner { get; set; }

		public IQueryable<WalletValue> Values { get; set; }

		private WalletValue GetCurrency(string currency)
		{
			return Values.First(w=>w.Currency.ToLower() == currency.ToLower());
		}

		#region IQueryable<WalletValue>

		public Type ElementType => ((IQueryable<WalletValue>)Values).ElementType;

		public Expression Expression => ((IQueryable<WalletValue>)Values).Expression;

		public IQueryProvider Provider => ((IQueryable<WalletValue>)Values).Provider;

		public IEnumerator<WalletValue> GetEnumerator()
		{
			return ((IQueryable<WalletValue>)Values).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IQueryable<WalletValue>)Values).GetEnumerator();
		}

		#endregion IQueryable<WalletValue>
	}
}