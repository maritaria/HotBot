namespace TwitchDungeon.Services.Irc
{
	public class IrcMessageReceived
	{
		public string Text { get; }

		public IrcMessageReceived(string message)
		{
			Text = message;
		}
	}
}