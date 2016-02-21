using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public sealed class PluginLoadedEvent
	{
		public Plugin Plugin { get; }

		public PluginLoadedEvent(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}