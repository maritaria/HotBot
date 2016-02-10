using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Defines a plugin for HotBot which can be loaded and unloaded at will.
	/// </summary>
	public interface Plugin
	{
		/// <summary>
		/// Gets the manager controlling the plugin
		/// </summary>
		PluginManager PluginManager { get; }

		/// <summary>
		/// Gets the description data of the plugin
		/// </summary>
		PluginDescription Description { get; }

		/// <summary>
		/// Loads the plugin
		/// </summary>
		void Load();

		/// <summary>
		/// Unloads the plugin, releases all managed and unmanaged resources
		/// </summary>
		void Unload();
	}
}