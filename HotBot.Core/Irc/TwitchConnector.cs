using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	public interface TwitchConnector
	{
		Credentials DefaultCredentials { get; set; }
		IrcConnection[] GroupServers { get; }
		WhisperConnection WhisperServer { get; }//TODO: Dictionary<Login, WhisperConnection> WhisperConnections{get;}
		LiveChannel[] ConnectedChannels { get; }
		ProtocolDecoder Decoder { get; }
		LiveChannel GetConnection(ChannelData channel);
	}
}