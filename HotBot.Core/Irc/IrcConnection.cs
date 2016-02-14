using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface IrcConnection
	{
		bool IsConnected { get; }

		void Connect(string hostname, ushort port);

		void Disconnect();

		void SendCommand(string ircCommand);

		void SendCommandBatch(params string[] ircCommands);

		Response ReadResponse();
	}
}