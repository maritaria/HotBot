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
		PluginManager Manager { get; }

		/// <summary>
		/// Gets the description data of the plugin
		/// </summary>
		PluginDescription Description { get; }
		
		/// <summary>
		/// Loads the plugin
		/// </summary>
		/// <param name="container">An IUnityContainer that can be used to spawn & register types</param>
		void Load(IUnityContainer container);//TODO: Create 2 interfaces; one for registering types and one for spawning them

		/// <summary>
		/// Unloads the plugin, releases all managed and unmanaged resources
		/// </summary>
		void Unload();
	}
}