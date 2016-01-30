using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using TwitchDungeon.Services.DataStorage;

namespace TwitchDungeon.Services.Irc
{
	public class IrcClient : IDisposable
	{
		private object _tcpClientLock = new object();
		private TcpClient _tcpClient;
		private NetworkStream _stream;
		private Thread _readerThread;
		private StreamReader _reader;
		private StreamWriter _writer;

		public string Hostname { get; private set; }
		public int Port { get; private set; }
		public string Username { get; private set; }
		public DataStore Database { get; }
		public MessageBus Bus { get; }

		public IrcClient(DataStore database, MessageBus bus)
		{
			Database = database;
			Bus = bus;
		}

		public void Connect(string hostname, UInt16 port)
		{
			Hostname = hostname;
			Port = port;
			//TODO: Cleanup
			lock (_tcpClientLock)
			{
				if (_tcpClient != null && _tcpClient.Connected)
				{
					throw new InvalidOperationException("IrcClient already connected");
				}
				try
				{
					_tcpClient = new TcpClient();
					_tcpClient.Connect(Hostname, Port);
					_stream = _tcpClient.GetStream();
					_reader = new StreamReader(_stream);
					_writer = new StreamWriter(_stream);
					_readerThread = new Thread(ReaderMethod);
					_readerThread.Name = "IrcClient.ReaderThread";
					_readerThread.Start();
				}
				catch
				{
					_tcpClient = null;
					_stream = null;
					_reader = null;
					_writer = null;
					_readerThread = null;

					throw;
				}
			}
		}

		public void SendMessage(string channelName, string format, params object[] args)
		{
			string message = string.Format(format, args);
			string command = string.Format(":{2}!{2}@{2}.tmi.twitch.tv PRIVMSG #{0} :{1}", channelName, message, Username);
			Send(command);
		}

		public void JoinChannel(string channelName)
		{
			EnsureChannel(channelName);
			Send(string.Format("JOIN #{0}", channelName));
		}

		private void EnsureChannel(string channelName)
		{
			GetChannel(channelName);
		}

		public Channel GetChannel(string channelName)
		{
			Channel channel = Database.Channels.FirstOrDefault(c => c.Name == channelName);
			if (channel == null)
			{
				channel = new Channel(channelName);
				Database.Channels.Add(channel);
				Database.SaveChanges();
			}
			else
			{

			}
			return channel;
		}

		public void Login(string username, string authKey)
		{
			Login(username, authKey, "*", "*");
		}

		public void Login(string username, string authKey, string hostname, string servername)
		{
			string passwordCommand = string.Format("PASS {0}", authKey);
			string userCommand = string.Format("USER {0} {1} {2}: {0}", username, hostname, servername);
			string nickCommand = string.Format("NICK {0}", username);
			Send(passwordCommand, userCommand, nickCommand);
			Username = username;
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
		
		private void Send(params string[] commands)
		{
			foreach (string line in commands)
			{
				_writer.WriteLine(line);
			}
			_writer.Flush();
		}

		private void ReaderMethod()
		{
			while (true)
			{
				//TODO: Make this async and allow for cancel mechanism for dispose pattern
				string message = _reader.ReadLine();
				Bus.Publish(new IrcMessageReceived(message));
			}
		}
	}
}