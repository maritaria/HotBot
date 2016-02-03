using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface IrcClientConfig
	{
		string Hostname { get; set; }
		ushort Port { get; set; }
		string Username { get; set; }
		string AuthKey { get; set; }
	}
}