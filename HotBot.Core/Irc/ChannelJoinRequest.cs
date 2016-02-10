using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	/// <summary>
	/// Publishable request that IrcClients are listening for.
	/// Causes the IrcClients that receive the request to join the specified channel.
	/// </summary>
	[DefaultPublishType(typeof(IrcTransmitRequest))]
	public sealed class ChannelJoinRequest : IrcTransmitRequest
	{
		/// <summary>
		/// The channel to join on the IRC server.
		/// </summary>
		public Channel Channel { get; }

		public override string IrcCommand => $"JOIN {Channel.Name}";

		public ChannelJoinRequest(Channel channel)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			Channel = channel;
		}
	}
}