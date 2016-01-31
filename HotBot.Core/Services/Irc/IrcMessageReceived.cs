using System;

namespace TwitchDungeon.Services.Irc
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