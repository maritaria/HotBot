using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	internal sealed class PluginInstanceCreatedEvent
	{
		public Plugin Plugin { get; }

		public PluginInstanceCreatedEvent(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}

	}
}
