using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicTwitchConnector : TwitchConnector
	{
		private Dictionary<ConnectionInfo, IrcConnection> _connections = new Dictionary<ConnectionInfo, IrcConnection>();
		private Dictionary<ChannelData, LiveChannel> _channels = new Dictionary<ChannelData, LiveChannel>();
		private DependencyOverrides _overrides = new DependencyOverrides();
		private WhisperConnection _whisperServer;

		public TwitchApi Api { get; }

		public Credentials DefaultCredentials { get; set; }

		public LiveChannel[] ConnectedChannels => _channels.Values.ToArray();

		public IrcConnection[] GroupServers => _connections.Values.ToArray();

		public IUnityContainer DependencyInjector { get; }

		public ProtocolDecoder Decoder { get; }
		
		public WhisperConnection WhisperServer
		{
			get
			{
				if (_whisperServer == null)
				{
					_whisperServer = CreateWhisperConnection();
				}
				return _whisperServer;
			}
		}

		public BasicTwitchConnector(TwitchApi api, IUnityContainer dependencyInjector)
		{
			Api = api;
			DependencyInjector = dependencyInjector;
		}

		public LiveChannel GetConnection(ChannelData channel)
		{
			if (!_channels.ContainsKey(channel))
			{
				_channels.Add(channel, CreateChannelConnection(channel));
			}
			return _channels[channel];
		}

		private LiveChannel CreateChannelConnection(ChannelData channel)
		{
			var connection = GetIrcConnection(channel);
			var overrides = new DependencyOverrides();
			overrides.Add(typeof(TwitchConnector), this);
			overrides.Add(typeof(IrcConnection), connection);
			overrides.Add(typeof(ChannelData), channel);
			return DependencyInjector.Resolve<LiveChannel>(overrides);
		}

		private IrcConnection GetIrcConnection(ChannelData channel)
		{
			var connections = Api.GetChatServers(channel);
			foreach (KeyValuePair<ConnectionInfo, IrcConnection> pair in _connections)
			{
				if (connections.Contains(pair.Key))
				{
					return pair.Value;
				}
			}
			return CreateIrcConnection(connections.First());
		}

		private IrcConnection CreateIrcConnection(ConnectionInfo connectionInfo)
		{
			var connection = DependencyInjector.Resolve<IrcConnection>();
			connection.Connect(connectionInfo);
			ApplyCredentials(connection);
			return connection;
		}

		private void ApplyCredentials(IrcConnection connection)
		{
			connection.SendCommandBatch(
				$"PASS {DefaultCredentials.AuthKey}",
				$"USER {DefaultCredentials.Username} * *: {DefaultCredentials.Username}",
				$"NICK {DefaultCredentials.Username}");
			connection.SendCommand("CAP REQ :twitch.tv/commands");
			connection.SendCommand("CAP REQ :twitch.tv/membership");
			connection.SendCommand("CAP END");
		}

		private WhisperConnection CreateWhisperConnection()
		{
			var connectionInfo = Api.GetWhisperServers().First();
			var connection = CreateIrcConnection(connectionInfo);
			var overrides = new DependencyOverrides();
			overrides.Add(typeof(TwitchConnector), this);
			overrides.Add(typeof(IrcConnection), connection);
			return DependencyInjector.Resolve<WhisperConnection>(overrides);
		}
	}
}