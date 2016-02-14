using System;
using System.Linq;

namespace HotBot.Core.Irc.Twitch
{
	public interface WhisperConnection
	{
		//https://discuss.dev.twitch.tv/t/whispers-on-irc/2459/2
		void StartListening(Channel channel);

		void StopListening(Channel channel);
	}

	namespace qq
	{
		//TODO: This version of the api is only possible if groupchat servers support normal chat as well
		//Otherwise ChannelConnection also needs a reference to the normal chat server connection

		public interface TwitchConnector
		{
			IrcConnection[] GroupServers { get; }
			ChannelConnection[] ConnectedChannels { get; }

			ChannelConnection GetConnection(Channel channel);

			void SetCredentials(TwitchLoginCredentials credentials);
		}

		public interface TwitchLoginCredentials
		{
			string Hostname { get; }
			ushort Port { get; }
			string Username { get; }
			string AuthKey { get; }
		}

		public interface ChannelConnection
		{
			Channel ChannelData { get; }
			TwitchConnector Connector { get; }
			IrcConnection Connection { get; }

			void Join();
			void Leave();
			void Say(string message);
		}

		public interface IrcConnection
		{
			void SendCommand(string command);

			Response ReadResponse();
		}
	}
}