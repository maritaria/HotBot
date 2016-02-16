using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface TwitchConnector
	{
		Credentials DefaultCredentials { get; set; }
		IrcConnection[] GroupServers { get; }
		WhisperConnection WhisperServer { get; }
		ChannelConnection[] ConnectedChannels { get; }
		ChannelConnection GetConnection(Channel channel);

		ProtocolDecoder Decoder { get; set; }

	}
}