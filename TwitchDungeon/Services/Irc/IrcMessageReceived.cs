namespace TwitchDungeon.Services.Irc
{
	internal class IrcMessageReceived
	{
		public string Text { get; }

		public IrcMessageReceived(string message)
		{
			Text = message;
		}
	}
}