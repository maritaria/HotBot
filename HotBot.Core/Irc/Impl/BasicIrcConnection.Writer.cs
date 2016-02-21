using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	public sealed partial class BasicIrcConnection
	{
		private class Writer : IDisposable
		{
			private StreamWriter _stream;
			private Queue<string> _queue = new Queue<string>();
			private object _queueLock = new object();
			private bool _disposed = false;

			public Writer(StreamWriter stream)
			{
				_stream = stream;
			}

			public void Queue(string message)
			{
				lock (_queueLock)
				{
					_queue.Enqueue(message);
					StartWrite();
				}
			}

			private void StartWrite()
			{
				string message = NextMessage();
				Console.WriteLine(message);
				_stream.WriteLine(message);
				_stream.Flush();
			}

			private string NextMessage()
			{
				lock (_queueLock)
				{
					return _queue.Dequeue();
				}
			}

			/*
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
						//_isWriting = false;
					}
				}
			}
			*/

			#region IDisposable Support

			protected virtual void Dispose(bool disposing)
			{
				lock (_queueLock)
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