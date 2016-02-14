using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Irc
{
	/// <summary>
	/// Encapsulates the hostmask of an IRC message.
	/// A hostmask looks like this: {Nickname}{}
	/// </summary>
	public sealed class HostMask
	{
		/// <summary>
		/// Gets or sets the nickname displayed in the hostmask
		/// </summary>
		public string Nickname { get; }

		/// <summary>
		/// Gets or sets the username displayed in the hostmask
		/// </summary>
		public string Username { get; }

		/// <summary>
		/// Gets or sets the hostname of the server that is displayed in the hostmask
		/// </summary>
		public string Hostname { get; }

		/// <summary>
		/// Decodes a hostmask into seperate user, nick and host namesy
		/// </summary>
		/// <param name="source">The string that contains the hostmask</param>
		public HostMask(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.StartsWith(":"))
			{
				source = source.Substring(1);
			}
			string[] parts = source.SplitMultiple("!", "@").ToArray();
			if (parts.Length == 1)
			{
				Nickname = parts[0];
				Username = parts[0];
				Hostname = parts[0];
			}
			else if (parts.Length == 3)
			{
				Nickname = parts[0];
				Username = parts[1];
				Hostname = parts[2];
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public override string ToString()
		{
			return $"{Nickname}!{Username}@{Hostname}";
		}
	}
}