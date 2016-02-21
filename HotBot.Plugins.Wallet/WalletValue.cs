using HotBot.Core.Irc;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class WalletValue
	{
		private const string UniqueConstraintName = "UniqueCurrencyPerOwner";

		[Index(UniqueConstraintName, 1, IsUnique = true)]
		public Guid OwnerId { get; set; }

		[Index(UniqueConstraintName, 2, IsUnique = true)]
		public string Currency { get; }

		public double Value { get; set; }

		protected WalletValue()
		{
		}

		public WalletValue(User owner, string currency) : this(owner, currency, 0)
		{
		}

		public WalletValue(User owner, string currency, double value)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (string.IsNullOrEmpty(currency))//TODO:Validate
			{
				throw new ArgumentException("Cannot be null or empty", "currency");
			}
			OwnerId = owner.Id;
			Currency = currency;
			Value = value;
		}
	}
}