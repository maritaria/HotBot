using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace HotBot.Core.Irc
{
	public class ThreadSafeIrcConnection : IrcConnection
	{
		private readonly object _communicationLock = new object();
		private TcpClient _tcpClient;
		private NetworkStream _stream;
		private StreamReader _reader;
		private StreamWriter _writer;

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
				
		public void Connect(string hostname, ushort port)
		{
			lock (_communicationLock)
			{
				if (IsConnected)
				{
					throw new InvalidOperationException("IrcClient already connected");
				}
				CleaupTcpClient();
				_tcpClient = new TcpClient(hostname, port);
				_stream = _tcpClient.GetStream();
				_reader = new StreamReader(_stream);
				_writer = new StreamWriter(_stream);
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
				_writer.WriteLine(ircCommand);
				_writer.Flush();
			}
		}

		public void SendCommandBatch(params string[] ircCommands)
		{
			lock (_communicationLock)
			{
				VerifyConnection();
				foreach (string command in ircCommands)
				{
					_writer.WriteLine(command);
				}
				_writer.Flush();
			}
		}

		public Response ReadResponse()
		{
			lock (_communicationLock)
			{
				VerifyConnection();
				string response = _reader.ReadLine();
				if (response == null)
				{
					return null;
				}
				return new Response(response);
			}
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
				if (_tcpClient.Connected)
				{
					_tcpClient.Close();
				}
				_tcpClient = null;
				_stream = null;
				_reader = null;
				_writer = null;
			}
		}
	}
}