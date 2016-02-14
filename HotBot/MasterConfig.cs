using HotBot.Core.Commands;
using HotBot.Core.Irc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace HotBot
{
	public class MasterConfig : ApplicationSettingsBase, CommandManagerConfig
	{
		[UserScopedSetting]
		[DefaultSettingValue("maritaria_bot01")]
		public string Username
		{
			get { return (string)this["Username"]; }
			set { this["Username"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("oauth:to4julsv3nu1c6lx9l1s13s7nj25yp")]
		public string AuthKey
		{
			get { return (string)this["AuthKey"]; }
			set { this["AuthKey"] = value; }
		}

		private IEnumerable<string> _cachedPrefixes;

		public IEnumerable<string> Prefixes
		{
			get
			{
				if (_cachedPrefixes == null)
				{
					_cachedPrefixes = PrefixesSerialized.Split(' ');
				}
				return _cachedPrefixes;
			}
			set
			{
				PrefixesSerialized = string.Join(" ", Prefixes);
				_cachedPrefixes = null;
			}
		}

		[DebuggerHidden]
		[UserScopedSetting]
		[DefaultSettingValue("!")]
		public string PrefixesSerialized
		{
			get { return (string)this["PrefixesSerialized"]; }
			set
			{
				this["PrefixesSerialized"] = value.Split(' ');
				_cachedPrefixes = null;
			}
		}
	}
}