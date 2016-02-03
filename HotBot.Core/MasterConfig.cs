using HotBot.Core.Irc;
using System;
using System.Configuration;
using System.Linq;

namespace HotBot.Core
{
	public class MasterConfig : ApplicationSettingsBase, IrcClientConfig
	{
		[UserScopedSetting]
		[DefaultSettingValue("irc.twitch.tv")]
		public string Hostname
		{
			get { return (string)this["Hostname"]; }
			set { this["Hostname"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("6667")]
		public ushort Port
		{
			get { return (ushort)this["Port"]; }
			set { this["Port"] = value; }
		}

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
	}
}