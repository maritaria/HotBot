using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicChannelUser : ChannelUser
	{
		public Channel Channel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public User GlobalUser
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
