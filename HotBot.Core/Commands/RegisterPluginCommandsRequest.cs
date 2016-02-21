using HotBot.Core.Plugins;
using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	[Obsolete]
	public sealed class RegisterPluginCommandsRequest
	{
		public Plugin Plugin { get; }

		public RegisterPluginCommandsRequest(Plugin plugin)
		{
			Verify.NotNull(plugin, "plugin");
			Plugin = plugin;
		}
	}
}