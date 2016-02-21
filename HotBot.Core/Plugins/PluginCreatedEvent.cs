using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public sealed class PluginCreatedEvent
	{
		public Plugin Plugin { get; }

		public PluginCreatedEvent(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}