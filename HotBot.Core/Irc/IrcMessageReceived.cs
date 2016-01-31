using System;

namespace HotBot.Core.Irc
{
	public class IrcMessageReceived
	{
		public string Message { get; }

		public IrcMessageReceived(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			Message = message;
		}
	}
}