using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotBot.Core.Irc
{
	public sealed class Response
	{
		//https://github.com/SirCmpwn/ChatSharp/blob/master/ChatSharp/IrcMessage.cs
		public HostMask HostMask { get; private set; }

		public string Command { get; private set; }
		public string[] Arguments { get; private set; }

		public Response(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			message = ReadHostmask(message);
			message = ReadCommand(message);
			ReadArguments(message);
		}

		internal string ReadHostmask(string message)
		{
			if (message.StartsWith(":"))
			{
				string[] prefixParts = message.SplitOnce(" ");
				HostMask = new HostMask(prefixParts[0]);
				message = prefixParts[1];
			}
			return message;
		}

		internal string ReadCommand(string message)
		{
			string[] parts = message.SplitOnce(" ");
			Command = parts[0];
			return parts[1];
		}

		internal void ReadArguments(string message)
		{
			List<string> arguments = new List<string>();
			while (message.Length > 0)
			{
				if (message.StartsWith(":"))
				{
					arguments.Add(message.Substring(1));
					break;
				}
				else
				{
					string[] parts = message.SplitOnce(" ");
					arguments.Add(parts[0]);
					message = parts[1];
				}
			}
			Arguments = arguments.ToArray();
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			if (HostMask != null)
			{
				builder.Append(':');
				builder.Append(HostMask.ToString());
				builder.Append(' ');
			}

			builder.Append(Command);
			foreach (string arg in Arguments)
			{
				builder.Append(' ');
				builder.Append(arg);
			}

			return builder.ToString();
		}
	}
}