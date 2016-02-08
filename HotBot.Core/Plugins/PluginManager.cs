using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Declares an interface for managing plugins.
	/// </summary>
	public interface PluginManager
	{
		/// <summary>
		/// Gets the object that used for loading the plugins
		/// </summary>
		PluginLoader Loader { get; }

		/// <summary>
		/// Registers a plugin to be managed by the current PluginManager.
		/// The plugin does not get automatically loaded.
		/// </summary>
		/// <param name="plugin">The plugin to be managed by the PluginManager</param>
		/// <exception cref="InvalidOperationException">Thrown if the plugin is already being managed by the current PluginManager</exception>
		void AddPlugin(LoadablePlugin plugin);

		/// <summary>
		/// Unregisters a plugin to be managed by the current PluginManager.
		/// The plugin is not automatically unloaded.
		/// </summary>
		/// <param name="plugin">The plugin to be managed by the PluginManager</param>
		/// <exception cref="InvalidOperationException">Thrown if the plugin isn't being managed by the current PluginManager</exception>
		void RemovePlugin(LoadablePlugin plugin);

		/// <summary>
		/// Gets a plugin instance by it's name.
		/// </summary>
		/// <param name="name">The name of the plugin to be found.</param>
		/// <returns>The plugin with the specified name. Null if none were found.</returns>
		LoadablePlugin GetPlugin(string name);
		/// <summary>
		/// Gets a plugin instance by it's type.
		/// The type is compared to the type of the instance, not by baseclasses or interfaces.
		/// </summary>
		/// <param name="type">The type of the plugin instance to find.</param>
		/// <returns>The plugin of the specified type. Null if none were found.</returns>
		LoadablePlugin GetPlugin(Type type);

		/// <summary>
		/// Loads all not-loaded plugins.
		/// </summary>
		void LoadAll();
		/// <summary>
		/// Unloads all loaded plugins.
		/// </summary>
		void UnloadAll();
		/// <summary>
		/// Unloads all loaded plugins and then loads them again.
		/// Does not load perviously unloaded plugins.
		/// </summary>
		void Reload();
	}
}