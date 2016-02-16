using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public interface ChannelState
	{
		Channel Channel { get; }
		IReadOnlyCollection<User> Users { get; }

		event EventHandler<ChannelUserEventArgs> UserJoined;
		event EventHandler<ChannelUserEventArgs> UserLeft;
	}
}
