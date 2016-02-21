using HotBot.Core.Plugins;
using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	[Obsolete]
	public sealed class UnregisterPluginCommandsRequest
	{
		public Plugin Plugin { get; }

		public UnregisterPluginCommandsRequest(Plugin plugin)
		{
			Verify.NotNull(plugin, "plugin");
			Plugin = plugin;
		}
	}
}