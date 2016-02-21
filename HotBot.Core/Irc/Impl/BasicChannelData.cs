using System;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannelData : ChannelData
	{
		public IQueryable<ChannelUser> KnownUsers { get; }

		public string Name { get; }

		public BasicChannelData(string name)
		{
			Name = name;
		}
	}
}