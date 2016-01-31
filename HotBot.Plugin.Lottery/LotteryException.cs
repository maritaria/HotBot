using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Plugin.Lottery
{
	public sealed class LotteryException : Exception
	{
		public LotteryException()
		{
		}

		public LotteryException(string message) : base(message)
		{
		}

		public LotteryException(string message, Exception innerException) : base(message, innerException)
		{
		}

		private LotteryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
