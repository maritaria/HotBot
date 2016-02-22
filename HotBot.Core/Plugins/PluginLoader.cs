using HotBot.Core.Intercom;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Declares an interface for loading plugins by name.
	/// </summary>
	public interface PluginLoader : Publisher
	{
		PluginManager Manager { get; set; }

		/// <summary>
		/// Gets the directory the plugins are loaded from.
		/// </summary>
		string PluginDirectory { get; }

		/// <summary>
		/// Loads all plugins from the plugin directory.
		/// Yield returns the plugins that are successfully loaded.
		/// </summary>
		void LoadPlugins();
	}
}