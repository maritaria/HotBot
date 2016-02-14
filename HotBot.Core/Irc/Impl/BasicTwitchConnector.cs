using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicTwitchConnector : TwitchConnector
	{
		private Dictionary<ConnectionInfo, IrcConnection> _connections = new Dictionary<ConnectionInfo, IrcConnection>();
		private Dictionary<Channel, ChannelConnection> _channels = new Dictionary<Channel, ChannelConnection>();
		private DependencyOverrides _overrides = new DependencyOverrides();

		public TwitchApi Api { get; }

		public Credentials DefaultCredentials { get; set; }

		public ChannelConnection[] ConnectedChannels => _channels.Values.ToArray();

		public IrcConnection[] GroupServers => _connections.Values.ToArray();

		public IUnityContainer DependencyInjector { get; }

		private WhisperConnection _whisperServer;

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

		public ChannelConnection GetConnection(Channel channel)
		{
			if (!_channels.ContainsKey(channel))
			{
				_channels.Add(channel, CreateChannelConnection(channel));
			}
			return _channels[channel];
		}

		private ChannelConnection CreateChannelConnection(Channel channel)
		{
			var connection = GetIrcConnection(channel);
			var overrides = new DependencyOverrides();
			overrides.Add(typeof(TwitchConnector), this);
			overrides.Add(typeof(IrcConnection), connection);
			overrides.Add(typeof(Channel), channel);
			return DependencyInjector.Resolve<ChannelConnection>(overrides);
		}

		private IrcConnection GetIrcConnection(Channel channel)
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