using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core
{
	/// <summary>
	/// Encapsulates the hostmask of an IRC message.
	/// </summary>
	public struct HostMask
	{
		public string Nickname;
		public string Username;
		public string Hostname;

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

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override string ToString()
		{
			return $"{Nickname}!{Username}@{Hostname}";
		}
	}
}
