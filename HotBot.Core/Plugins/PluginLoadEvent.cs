using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Published when a plugin instance has been loaded and is ready for use
	/// </summary>
	public sealed class PluginLoadEvent
	{
		/// <summary>
		/// The plugin that has loaded
		/// </summary>
		public LoadablePlugin Plugin { get; }

		public PluginLoadEvent(LoadablePlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}