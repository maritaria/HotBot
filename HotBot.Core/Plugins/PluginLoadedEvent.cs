using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	public sealed class PluginLoadedEvent
	{
		public LoadablePlugin Plugin { get; }

		public PluginLoadedEvent(LoadablePlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}

	}
}
