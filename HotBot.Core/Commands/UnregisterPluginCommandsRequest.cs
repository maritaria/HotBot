using HotBot.Core.Plugins;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public sealed class UnregisterPluginCommandsRequest
	{
		public Plugin Plugin { get; }

		public UnregisterPluginCommandsRequest(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}