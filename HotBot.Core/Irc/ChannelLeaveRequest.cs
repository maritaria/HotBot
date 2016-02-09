using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	//TODO: Create IrcChannelCommandRequest for all request involving a channel
	[DefaultPublishType(typeof(IrcTransmitRequest))]
	public sealed class ChannelLeaveRequest : IrcTransmitRequest
	{
		/// <summary>
		/// The channel to join on the IRC server.
		/// </summary>
		public Channel Channel { get; }

		public override string IrcCommand => $"JOIN {Channel.Name}";

		public ChannelLeaveRequest(Channel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			Channel = channel;
		}
	}
}
