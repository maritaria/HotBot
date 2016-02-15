using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace HotBot.Core.Irc.Impl
{
	public sealed partial class BasicIrcConnection
	{
		private class Writer : IDisposable
		{
			private NetworkStream _stream;
			private Queue<string> _queue = new Queue<string>();
			private bool _isWriting = false;
			private object _queueLock = new object();
			private bool _disposed = false;
			public Encoding Encoding { get; set; }

			public Writer(NetworkStream stream)
			{
				_stream = stream;
			}

			public void Queue(string message)
			{
				lock (_queueLock)
				{
					_queue.Enqueue(message);
					if (!_isWriting)
					{
						_isWriting = true;
						StartWrite();
					}
				}
			}

			private void StartWrite()
			{
				string message = NextMessage();
				byte[] encoded = Encode(message);
				_stream.BeginWrite(encoded, 0, encoded.Length, WriteCompleted, null);
			}

			private string NextMessage()
			{
				lock (_queueLock)
				{
					return _queue.Dequeue();
				}
			}

			private byte[] Encode(string message)
			{
				return Encoding.GetBytes(message);
			}


			private void WriteCompleted(IAsyncResult ar)
			{
				_stream.EndWrite(ar);
				lock (_queueLock)
				{
					if (_queue.Count > 0 && !_disposed)
					{
						StartWrite();
					}
					else
					{
						_isWriting = false;
					}
				}
			}

			#region IDisposable Support

			protected virtual void Dispose(bool disposing)
			{
				lock(_queueLock)
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
			}

			~Writer()
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
			}

			private void DisposeManaged()
			{
			}

			#endregion IDisposable Support
		}
	}
}