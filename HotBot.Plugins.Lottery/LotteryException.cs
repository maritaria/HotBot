using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HotBot.Plugins.Lottery
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