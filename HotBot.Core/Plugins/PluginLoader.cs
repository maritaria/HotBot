namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Declares an interface for loading plugins by name.
	/// </summary>
	public interface PluginLoader
	{
		/// <summary>
		/// Gets the directory the plugins are loaded from.
		/// </summary>
		string PluginDirectory { get; }

		/// <summary>
		/// Loads all plugins from the plugin directory.
		/// </summary>
		void LoadPlugins();
	}
}