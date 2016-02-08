using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public class ReflectionPluginManager : PluginManager
	{
		private Dictionary<string, LoadablePlugin> _namedPlugins = new Dictionary<string, LoadablePlugin>();
		private Dictionary<Type, LoadablePlugin> _typedPlugins = new Dictionary<Type, LoadablePlugin>();
		
		public PluginLoader Loader { get; }

		public ReflectionPluginManager(PluginLoader loader)
		{
			if (loader == null)
			{
				throw new ArgumentNullException("loader");
			}
			Loader = loader;
		}

		public void AddPlugin(LoadablePlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			if (plugin.Description == null)
			{
				throw new ArgumentException("Plugin.Description cannot be null", "plugin");
			}
			Type pluginType = plugin.GetType();
			string pluginName = plugin.Description.Name;
			if (_typedPlugins.ContainsKey(pluginType))
			{
				throw new InvalidOperationException($"A plugin by the type '{pluginType.FullName}' has already been added");
			}
			if (_namedPlugins.ContainsKey(pluginName))
			{
				throw new InvalidOperationException($"Another plugin by the name '{pluginName}' has already been added. It is of the type '{pluginType.FullName}");
			}
			_typedPlugins.Add(pluginType, plugin);
			_namedPlugins.Add(pluginName, plugin);
		}

		public void RemovePlugin(LoadablePlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			if (plugin.Description == null)
			{
				throw new ArgumentException("Plugin.Description cannot be null", "plugin");
			}
			Type pluginType = plugin.GetType();
			string pluginName = plugin.Description.Name;
			if (_typedPlugins.ContainsKey(pluginType))
			{
				_typedPlugins.Remove(pluginType);
				_namedPlugins.Remove(pluginName);
			}
		}

		public LoadablePlugin GetPlugin(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("cannot be empty", "name");
			}
			if (!_namedPlugins.ContainsKey(name))
			{
				return null;
			}
			return _namedPlugins[name];
		}

		public LoadablePlugin GetPlugin(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!_typedPlugins.ContainsKey(type))
			{
				return null;
			}
			return _typedPlugins[type];
		}

		public void LoadAll()
		{
			Loader.LoadPlugins();
			foreach (LoadablePlugin pl in _typedPlugins.Values)
			{
				try
				{
					pl.Load();//TODO: try-catch loading of plugins
				}
				catch (Exception ex)
				{
					//TODO: throw new PluginException();
				}
			}
		}

		public void UnloadAll()
		{
			foreach (LoadablePlugin pl in _typedPlugins.Values)
			{
				try
				{
					pl.Unload();//TODO: try-catch unloading of plugins
				}
				catch (Exception ex)
				{
					//TODO: throw new PluginException();
				}
			}
		}

		public void Reload()
		{
			LoadAll();
			UnloadAll();
		}
	}
}