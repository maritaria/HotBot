namespace HotBot.Core.Irc
{
	public interface LoginMethod
	{
		void Login(IrcConnection connection);
	}
}