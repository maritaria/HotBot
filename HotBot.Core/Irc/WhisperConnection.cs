using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface WhisperConnection : Publisher, IDisposable
	{
		TwitchConnector Connector { get; }
		IrcConnection Connection { get; }

		void SendWhisper(User user, string message);

		bool IsDisposed { get; }
	}
}