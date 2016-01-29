using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace TwitchDungeon
{
	public class IrcClient : IDisposable
	{
		private object _tcpClientLock = new object();
		private TcpClient _tcpClient;
		private NetworkStream _stream;
		private Thread _readerThread;
		private StreamReader _reader;
		private StreamWriter _writer;

		public string Hostname { get; }
		public int Port { get; }
		public string Username { get; private set; }

		public IrcClient(string hostname, UInt16 port)
		{
			Hostname = hostname;
			Port = port;
		}

		public void Connect()
		{
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
			Send(string.Format("JOIN #{0}", channelName));
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

		public event EventHandler<IrcChatMessageEventArgs> ChatMessageReceived;

		protected virtual void OnChatMessageReceived(IrcChatMessageEventArgs e)
		{
			ChatMessageReceived?.Invoke(this, e);
		}

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
				HandleMessage(message);
			}
		}

		private void HandleMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentException("cannot be empty or null", "message");
			}
			Console.WriteLine(message);

			if (!message.Contains("PRIVMSG"))
			{
				return;
			}
			//:USERNAME!USERNAME@USERNAME.tmi.twitch.tv PRIVMSG #CHANNEL :MESSAGE
			message = message.Substring(1);
			int usernameLength = 0;
			for (int i = 0; i < message.Length; i++)
			{
				char c = message[i];
				if (c == '!')
				{
					usernameLength = i;
					break;
				}
			}
			//TODO: Chain of responsibility
			string[] parts = SplitOnFirstOccurence(message, "PRIVMSG");
			string username = parts[0].Substring(0, usernameLength);
			string chatDetails = parts[1].TrimStart(' ').Substring(1);
			string[] chatParts = SplitOnFirstOccurence(chatDetails, " :");
			string channel = chatParts[0];
			string chatmessage = chatParts[1];

			OnChatMessageReceived(new IrcChatMessageEventArgs(this, channel, username, chatmessage));
		}

		private string[] SplitOnFirstOccurence(string source, string splitter)
		{
			string[] parts = source.Split(new string[] { splitter }, StringSplitOptions.None);
			string remainder = string.Join(splitter, parts.Skip(1));
			return new string[] { parts[0], remainder };
		}
	}
}