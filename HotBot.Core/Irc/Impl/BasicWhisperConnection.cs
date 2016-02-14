using System;
using System.Linq;
using System.Threading;

namespace HotBot.Core.Irc.Impl
{
	public sealed class BasicWhisperConnection : WhisperConnection
	{
		private Thread _readerThread;

		public IrcConnection Connection { get; }
		public TwitchConnector Connector { get; }
		public MessageBus Bus { get; }

		public BasicWhisperConnection(TwitchConnector connector, IrcConnection connection, MessageBus bus)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("connector");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Connector = connector;
			Connection = connection;
			Bus = bus;
			StartReaderThread();
		}

		public void SendWhisper(User user, string message)
		{
		}


		private void StartReaderThread()
		{
			_readerThread = new Thread(new ThreadStart(ReaderThreadMethod));
			_readerThread.Name = $"BasicWhisperConnection.ReaderThread";
			_readerThread.Start();
		}

		private void ReaderThreadMethod()
		{
			while (true)
			{
				Response r = Connection.ReadResponse();
				Bus.Publish(r);
			}
		}

		private void StopReaderThread()
		{
			//TODO: Make API async
		}



		#region IDisposable Support

		public bool IsDisposed { get; private set; }

		~BasicWhisperConnection()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

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

		private void DisposeUnmanaged()
		{
		}

		private void DisposeManaged()
		{
		}

		#endregion IDisposable Support
	}
}