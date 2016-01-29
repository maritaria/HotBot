using System;

namespace TwitchDungeon
{
	public sealed class IrcChatMessageEventArgs : EventArgs
	{
		public IrcClient Client { get; }
		public string Channel { get; }
		public string Username { get; }
		public string Message { get; }

		public IrcChatMessageEventArgs(IrcClient client, string channel, string username, string message)
		{
			Client = client;
			Channel = channel;
			Username = username;
			Message = message;
		}
	}
}