using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HotBot.Core.Irc.Impl
{
	public sealed partial class BasicIrcConnection
	{
		private class Reader : IDisposable
		{
			private NetworkStream _stream;
			private byte[] _buffer = new byte[BasicIrcConnection.ReceiveBufferSize];
			private bool _disposed = false;
			public Encoding Encoding { get; set; }

			public Reader(NetworkStream stream)
			{
				_stream = stream;
				StartRead();
			}
			
			private void StartRead()
			{
				_stream.BeginRead(_buffer, 0, _buffer.Length, ReadCompleted, null);
			}

			private void ReadCompleted(IAsyncResult ar)
			{
				int bytesRead = _stream.EndRead(ar);
				if (bytesRead > 0)
				{
					DataReceived(_buffer, bytesRead);
					StartRead();
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
				//TODO: Handle it
				throw new NotImplementedException();
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
			}

			private void DisposeManaged()
			{
			}

			#endregion IDisposable Support
		}
	}
}