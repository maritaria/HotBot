using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Commands
{
	public sealed class InvalidCommandNameException : Exception
	{
		public InvalidCommandNameException(string message) : base(message)
		{
		}

		public InvalidCommandNameException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
