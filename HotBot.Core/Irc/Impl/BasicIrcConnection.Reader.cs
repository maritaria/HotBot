using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HotBot.Core.Irc.Impl
{
	public sealed partial class BasicIrcConnection
	{
		private class Reader : IDisposable
		{
			private StreamReader _stream;
			private byte[] _buffer = new byte[BasicIrcConnection.ReceiveBufferSize];
			private bool _disposed = false;
			private BasicIrcConnection _owner;
			private CancellationTokenSource _cancellation = new CancellationTokenSource();
			private Thread _readerThread;
			public Encoding Encoding { get; set; } = Encoding.UTF8;

			public Reader(BasicIrcConnection owner, StreamReader stream)
			{
				_owner = owner;
				_stream = stream;
				StartReaderThread();
			}

			private void StartReaderThread()
			{
				_readerThread = new Thread(ReaderThreadMethod);
				_readerThread.Name = "BasicIrcConnection.Reader";
				_readerThread.Start();
			}

			private void StopReaderThread()
			{
				_cancellation.Cancel();
			}

			private void ReaderThreadMethod()
			{
				while (!_cancellation.IsCancellationRequested)
				{
					HandleReceivedData(_stream.ReadLine());
					/*
					var task = _stream.ReadAsync(_buffer, 0, _buffer.Length, _cancellation.Token);
					task.Wait(_cancellation.Token);
					if (!task.IsCanceled)
					{
						int bytesRead = task.Result;
						if (bytesRead > 0)
						{
							DataReceived(_buffer, bytesRead);
						}
					}
					*/
				}
			}

			private void DataReceived(byte[] buffer, int length)
			{
				string message = Decode(buffer, length);
				HandleReceivedData(message);
			}

			private string Decode(byte[] buffer, int length)
			{
				return Encoding.GetString(buffer, 0, length);
			}

			private void HandleReceivedData(string message)
			{
				Response r = new Response(message);
				Console.WriteLine(r);
				_owner.HandleResponse(r);
			}

			#region IDisposable Support

			protected virtual void Dispose(bool disposing)
			{
				if (!_disposed)
				{
					if (disposing)
					{
						DisposeManaged();
					}
					DisposeUnmanaged();
					_disposed = true;
				}
			}

			~Reader()
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
				StopReaderThread();
			}

			private void DisposeManaged()
			{
			}

			#endregion IDisposable Support
		}
	}
}