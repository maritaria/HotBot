using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace HotBot.Core.Irc
{
	//TODO: create smaller classes that handle these messages? or keep growing the irc client class which will result in mammoth classes.
	public class IrcClient : IDisposable,
		MessageHandler<IrcTransmitRequest>,
		MessageHandler<ChatTransmitRequest>,
		MessageHandler<ChannelJoinRequest>,
		MessageHandler<ChannelNotificationRequest>,
		MessageHandler<RegisterCapabilityRequest>,
		MessageHandler<ChangeColorRequest>
	{
		//https://github.com/SirCmpwn/ChatSharp
		private object _tcpClientLock = new object();

		private object _writerLock = new object();
		private object _stateLock = new object();

		private TcpClient _tcpClient;
		private NetworkStream _stream;
		private Thread _readerThread;
		private StreamReader _reader;
		private StreamWriter _writer;
		private CancellationTokenSource _readerCancellation = new CancellationTokenSource();
		private Dictionary<string, Channel> _joinedChannels = new Dictionary<string, Channel>();

		public IReadOnlyDictionary<string, Channel> JoinedChannels { get; }

		public MessageBus Bus { get; }
		public DataStore DataStore { get; }
		public IrcClientConfig Config { get; }

		public bool IsConnected => _tcpClient != null && _tcpClient.Connected;

		public IrcClient(MessageBus bus, DataStore dataStore, IrcClientConfig config)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (dataStore == null)
			{
				throw new ArgumentNullException("dataStore");
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			JoinedChannels = new ReadOnlyDictionary<string, Channel>(_joinedChannels);
			Bus = bus;
			//TODO: Generalize this, maybe something with attributed methods YES MAKE THIS HAPPEN
			Bus.Subscribe<IrcTransmitRequest>(this);
			Bus.Subscribe<ChatTransmitRequest>(this);
			Bus.Subscribe<ChannelJoinRequest>(this);
			Bus.Subscribe<ChannelNotificationRequest>(this);
			Bus.Subscribe<RegisterCapabilityRequest>(this);
			Bus.Subscribe<ChangeColorRequest>(this);
			DataStore = dataStore;
			Config = config;
			Connect();
		}

		public void Connect()
		{
			lock (_tcpClientLock)
			{
				if (IsConnected)
				{
					throw new InvalidOperationException("IrcClient already connected to a server");
				}
				InitializeConnection();
				StartReaderThread();
			}
			Login();
		}

		private void InitializeConnection()
		{
			_tcpClient = new TcpClient();
			_tcpClient.Connect(Config.Hostname, Config.Port);
			_stream = _tcpClient.GetStream();
			_reader = new StreamReader(_stream);
			_writer = new StreamWriter(_stream);
		}

		public Channel JoinChannel(string channelName)
		{
			Channel channel = GetChannel(channelName);
			JoinChannel(channel);
			return channel;
		}

		public void JoinChannel(Channel channel)
		{
			SendCommand($"JOIN {channel}");
		}

		public void LeaveChannel(Channel channel)
		{
			SendCommand($"PART {channel}");
		}

		private void EnsureChannel(string channelName)
		{
			GetChannel(channelName);
		}

		public Channel GetChannel(string channelName)
		{
			Channel channel = DataStore.Channels.FirstOrDefault(c => c.Name == channelName);
			if (channel == null)
			{
				channel = new Channel(channelName);
				DataStore.Channels.Add(channel);
				DataStore.SaveChanges();
			}
			return channel;
		}

		public void Login()
		{
			string authKey = Config.AuthKey;
			string username = Config.Username;
			SendBatch($"PASS {authKey}", $"USER {username} * *: {username}", $"NICK {username}");
		}

		public void Logout()
		{
		}

		public void SendCommand(string command)
		{
			lock (_writerLock)
			{
				_writer.WriteLine(command);
				_writer.Flush();
			}
		}

		public void SendBatch(params string[] commands)
		{
			lock (_writerLock)
			{
				foreach (string command in commands)
				{
					_writer.WriteLine(command);
				}
				_writer.Flush();
			}
		}

		private void StartReaderThread()
		{
			_readerThread = new Thread(ReaderMethod);
			_readerThread.Name = "IrcClient.ReaderThread";
			_readerThread.Start();
			_readerCancellation = new CancellationTokenSource();
		}

		private void StopReaderThread()
		{
			_readerCancellation.Cancel();
		}

		private void ReaderMethod()
		{
			var token = _readerCancellation.Token;
			while (true)
			{
				var readTask = _reader.ReadLineAsync();
				try
				{
					readTask.Wait(token);
				}
				catch (OperationCanceledException)
				{
					return;
				}
				string message = readTask.Result;
				if (message != null)
				{
					Bus.Publish(new IrcReceivedEvent(message));
				}
				else
				{
					Console.WriteLine("> Connection lost");
					Bus.Publish(new ConnectionLostEvent(this));
					break;
				}
			}
		}
		//TODO: [Subscribe]
		//TODO: [Subscribe(typeof(IrcTransmitRequest))]
		public void OnIrcTransmitRequest(IrcTransmitRequest request)
		{

		}

		#region IDisposable Support

		public bool IsDisposed { get; private set; } = false;

		private void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					DisposeManaged();
				}

				DisposeUnmanaged();

				IsDisposed = true;
			}
		}

		~IrcClient()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void DisposeUnmanaged()
		{
			_tcpClient?.Close();
			_readerThread?.Abort();
		}

		private void DisposeManaged()
		{
		}

		public event EventHandler Disposing;

		private void OnDisposing()
		{
			Disposing?.Invoke(this, EventArgs.Empty);
		}

		#endregion IDisposable Support

		void MessageHandler<IrcTransmitRequest>.HandleMessage(IrcTransmitRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			SendCommand(message.IrcCommand);
		}

		void MessageHandler<ChatTransmitRequest>.HandleMessage(ChatTransmitRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			(this as MessageHandler<IrcTransmitRequest>).HandleMessage(message);
		}

		void MessageHandler<ChannelJoinRequest>.HandleMessage(ChannelJoinRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			JoinChannel(message.Channel);
		}

		void MessageHandler<ChannelNotificationRequest>.HandleMessage(ChannelNotificationRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			(this as MessageHandler<IrcTransmitRequest>).HandleMessage(message);
		}

		void MessageHandler<RegisterCapabilityRequest>.HandleMessage(RegisterCapabilityRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			(this as MessageHandler<IrcTransmitRequest>).HandleMessage(message);
		}

		void MessageHandler<ChangeColorRequest>.HandleMessage(ChangeColorRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			(this as MessageHandler<IrcTransmitRequest>).HandleMessage(message);
		}
	}
}