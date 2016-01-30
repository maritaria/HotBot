using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Messages
{
	public sealed class InvalidHandlerArgumentException : ArgumentException
	{
		public InvalidHandlerArgumentException()
		{
		}

		public InvalidHandlerArgumentException(string message) : base(message)
		{
		}

		public InvalidHandlerArgumentException(string message, string paramName) : base(message, paramName)
		{
		}

		public InvalidHandlerArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public InvalidHandlerArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
		{
		}

		private InvalidHandlerArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
