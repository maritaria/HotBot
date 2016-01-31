using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public class ReflectionPluginManager : PluginManager
	{
		private Dictionary<string, Plugin> _namedPlugins = new Dictionary<string, Plugin>();
		private Dictionary<Type, Plugin> _typedPlugins = new Dictionary<Type, Plugin>();
		private object _stateLock = new object();

		public PluginManagerState State { get; private set; } = PluginManagerState.Inactive;
		public IUnityContainer Container { get; private set; }

		public ReflectionPluginManager(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			Container = container;
		}

		public void AddPlugin(Plugin plugin)
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
			if (!_typedPlugins.ContainsKey(pluginType))
			{
				_typedPlugins.Add(pluginType, plugin);
				_namedPlugins.Add(pluginName, plugin);
			}
		}

		public void RemovePlugin(Plugin plugin)
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
				if (_namedPlugins.ContainsKey(pluginName))
				{
					throw new ArgumentException($"There already is a plugin by the name of '{pluginName}'");
				}
				_typedPlugins.Add(pluginType, plugin);
				_namedPlugins.Add(pluginName, plugin);
			}
		}

		public Plugin GetPlugin(string name)
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

		public Plugin GetPlugin(Type type)
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

		public TPlugin GetPlugin<TPlugin>() where TPlugin : Plugin
		{
			return (TPlugin)GetPlugin(typeof(TPlugin));
		}

		public void Startup()
		{
			lock (_stateLock)
			{
				if (State == PluginManagerState.Active)
				{
					throw new InvalidOperationException("PluginManager is currently active");
				}
				foreach (Plugin pl in _typedPlugins.Values)
				{
					pl.Load(Container);//TODO: try-catch
				}
				State = PluginManagerState.Active;
			}
		}

		public void Shutdown()
		{
			lock (_stateLock)
			{
				if (State == PluginManagerState.Inactive)
				{
					throw new InvalidOperationException("PluginManager is currently inactive");
				}
				foreach (Plugin pl in _typedPlugins.Values)
				{
					pl.Unload();//TODO: try-catch
				}
				State = PluginManagerState.Inactive;
			}
		}

		public void Restart()
		{
			if (State == PluginManagerState.Active)
			{
				Shutdown();
			}
			else
			{
				Startup();
			}
		}
	}
}