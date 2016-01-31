using System;

namespace HotBot.Core.Irc
{
	public class IrcReceivedEvent
	{
		public string Message { get; }

		public IrcReceivedEvent(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			Message = message;
		}
	}
}