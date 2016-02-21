using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicTwitchConnector : TwitchConnector
	{
		private Dictionary<ConnectionInfo, IrcConnection> _connections = new Dictionary<ConnectionInfo, IrcConnection>();
		private Dictionary<string, LiveChannel> _channels = new Dictionary<string, LiveChannel>();
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

		public LiveChannel GetConnection(string channelName)
		{
			if (!_channels.ContainsKey(channelName))
			{
				_channels.Add(channelName, CreateChannelConnection(channelName));
			}
			return _channels[channelName];
		}

		private LiveChannel CreateChannelConnection(string channelName)
		{
			var connection = GetIrcConnection(channelName);
			var overrides = new DependencyOverrides();
			overrides.Add(typeof(TwitchConnector), this);
			overrides.Add(typeof(IrcConnection), connection);
			return DependencyInjector.Resolve<LiveChannel>(overrides, new ParameterOverride("channelName", channelName));//TODO: Encapsulate parameters
		}

		private IrcConnection GetIrcConnection(string channelName)
		{
			var connections = Api.GetChatServers(channelName);
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
			ApplyPingResponder(connection);
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

		private void ApplyPingResponder(IrcConnection connection)
		{
			new PingResponder(connection);
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