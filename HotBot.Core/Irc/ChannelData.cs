using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public interface ChannelData
	{
		/// <summary>
		/// Users who at some point have joined the <see cref="Channel"/>.
		/// </summary>
		IQueryable<ChannelUser> KnownUsers { get; }
	}
}
