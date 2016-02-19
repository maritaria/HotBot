using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Plugins.Wallet
{
	public interface WalletValue
	{
		string Currency { get; }
		int Value { get; set; }
	}
}
