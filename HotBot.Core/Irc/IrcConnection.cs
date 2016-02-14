using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface IrcConnection
	{
		bool IsConnected { get; }

		void Connect(ConnectionInfo info);

		void Disconnect();

		void SendCommand(string ircCommand);

		void SendCommandBatch(params string[] ircCommands);

		Response ReadResponse();

	}
}