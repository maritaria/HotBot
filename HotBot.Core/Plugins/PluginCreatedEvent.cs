using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
