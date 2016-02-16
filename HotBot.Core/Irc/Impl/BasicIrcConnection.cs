using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Impl
{
	public sealed partial class BasicIrcConnection : IrcConnection
	{
		public const int ReceiveBufferSize = 4096;
		public const int TransmitBufferSize = 4096;

		private readonly object _communicationLock = new object();
		private bool _stopReaderThread = false;
		private TcpClient _tcpClient;
		private NetworkStream _stream;
		private Reader _reader;
		private Writer _writer;
		private byte[] TransmitBuffer = new byte[TransmitBufferSize];
		private byte[] ReceiveBuffer = new byte[ReceiveBufferSize];

		public MessageBus Bus { get; }
		public Encoding Encoding { get; set; } = Encoding.ASCII;

		public bool IsConnected
		{
			get
			{
				lock (_communicationLock)
				{
					return _tcpClient != null && _tcpClient.Connected;
				}
			}
		}

		public BasicIrcConnection(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
		}

		public event EventHandler<ResponseEventArgs> ResponseReceived;

		public void Connect(ConnectionInfo info)
		{
			lock (_communicationLock)
			{
				if (IsConnected)
				{
					throw new InvalidOperationException("IrcClient already connected");
				}
				CleaupTcpClient();
				_tcpClient = new TcpClient(info.Hostname, info.Port);
				_stream = _tcpClient.GetStream();
				_reader = new Reader(this, new StreamReader(_stream));
				_writer = new Writer(new StreamWriter(_stream));
			}
		}

		public void Disconnect()
		{
			lock (_communicationLock)
			{
				VerifyConnection();
				CleaupTcpClient();
			}
		}

		public void SendCommand(string ircCommand)
		{
			lock (_communicationLock)
			{
				VerifyConnection();
				_writer.Queue(ircCommand);
			}
		}

		public void SendCommandBatch(params string[] ircCommands)
		{
			lock (_communicationLock)
			{
				VerifyConnection();
				foreach(string com in ircCommands)
				{
					_writer.Queue(com);
				}
				//string block = string.Join("\n", ircCommands);
				//_writer.Queue(block);
			}
		}

		public Response ReadResponse()
		{
			throw new NotImplementedException();
		}

		private void VerifyConnection()
		{
			if (!IsConnected)
			{
				throw new InvalidOperationException("Not connected to a server");
			}
		}

		private void CleaupTcpClient()
		{
			if (_tcpClient != null)
			{
				_reader.Dispose();
				_writer.Dispose();
				_tcpClient.Close();
				_tcpClient = null;
				_stream = null;
				_reader = null;
				_writer = null;
			}
		}

		private void HandleResponse(Response response)
		{
			ResponseReceived?.Invoke(this, new ResponseEventArgs(this, response));
		}

	}
}