using HotBot.Core.Irc;
using HotBot.Core.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotBot.Plugins.Wallet
{
	internal class UserWallet
	{
		private const string UniqueConstraintName = "UniqueCurrencyPerOwner";

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Index(UniqueConstraintName, 1, IsUnique = true)]
		public Guid UserId { get; set; }

		[MaxLength(Verify.MaximumCurrencyLength)]
		[Index(UniqueConstraintName, 2, IsUnique = true)]
		public string Currency { get; }

		public double Value { get; set; }

		protected UserWallet()
		{
		}

		public UserWallet(User user, string currency) : this(user, currency, 0)
		{
		}

		public UserWallet(User user, string currency, double value)
		{
			Verify.NotNull(user, "user");
			Verify.Currency(currency, "currency");
			UserId = user.Id;
			Currency = currency;
			Value = value;
		}
	}
}