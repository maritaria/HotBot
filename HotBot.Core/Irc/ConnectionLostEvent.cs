using System;

namespace HotBot.Core.Irc
{
	public sealed class ConnectionLostEvent
	{
		public IrcClient IrcClient { get; }

		public ConnectionLostEvent(IrcClient ircClient)
		{
			if (ircClient == null)
			{
				throw new ArgumentNullException("ircClient");
			}
			IrcClient = ircClient;
		}
	}
}