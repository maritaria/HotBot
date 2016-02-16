using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
