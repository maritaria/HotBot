using HotBot.Core.Intercom;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface WhisperConnection : Publisher
	{
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }

		void SendWhisper(User user, string message);

		event EventHandler<WhisperEventArgs> WhisperReceived;
	}
}