using HotBot.Core.Plugins;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public sealed class UnregisterPluginCommandsRequest
	{
		public LoadablePlugin Plugin { get; }

		public UnregisterPluginCommandsRequest(LoadablePlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}