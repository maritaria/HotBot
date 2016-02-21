using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface ChannelData
	{
		string Name { get; }

		/// <summary>
		/// Gets a querryable source holding all users who at some point have joined the <see cref="ChannelData"/>.
		/// </summary>
		IQueryable<ChannelUser> KnownUsers { get; }
	}
}