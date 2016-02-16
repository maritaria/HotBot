using System;

namespace HotBot.Core.Irc
{
	public interface ChannelUser
	{
		/// <summary>
		/// Gets the channel the instance relates to.
		/// </summary>
		Channel Channel { get; }

		/// <summary>
		/// Gets the global instance of the user.
		/// </summary>
		User GlobalUser { get; }
	}
}