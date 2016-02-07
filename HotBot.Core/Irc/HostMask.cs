using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	/// <summary>
	/// Encapsulates the hostmask of an IRC message.
	/// A hostmask looks like this: {Nickname}{}
	/// </summary>
	public struct HostMask
	{
		/// <summary>
		/// Gets or sets the nickname displayed in the hostmask
		/// </summary>
		public string Nickname;
		/// <summary>
		/// Gets or sets the username displayed in the hostmask
		/// </summary>
		public string Username;
		/// <summary>
		/// Gets or sets the hostname of the server that is displayed in the hostmask
		/// </summary>
		public string Hostname;
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
			string[] parts = source.SplitMultiple("!", "@").ToArray();
			//TODO: More validation
			//TODO: Create new IRC related project
			Nickname = parts[0];
			Username = parts[1];
			Hostname = parts[2];
		}

		public override string ToString()
		{
			return $"{Nickname}!{Username}@{Hostname}";
		}
	}
}
