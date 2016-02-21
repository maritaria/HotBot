using HotBot.Core.Intercom;
using HotBot.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Commands
{
	[Obsolete]
	public class CommandRedirecter
	{
		private Dictionary<string, HashSet<CommandListener>> _listeners = new Dictionary<string, HashSet<CommandListener>>();
		private object _listenersLock = new object();

		public MessageBus Bus { get; }

		public CommandRedirecter(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			Bus = bus;
			Bus.Subscribe(this);
		}

		public void AddListener(string commandName, CommandListener listener)
		{
			try
			{
				CommandEvent.VerifyCommandName(commandName);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "commandName", ex);
			}
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			commandName = commandName.ToLower();
			lock (_listenersLock)
			{
				if (!_listeners.ContainsKey(commandName))
				{
					var listeners = new HashSet<CommandListener>();
					listeners.Add(listener);
					_listeners[commandName] = listeners;
				}
				else
				{
					_listeners[commandName].Add(listener);
				}
			}
		}

		public void RemoveListener(string commandName, CommandListener listener)
		{
			try
			{
				CommandEvent.VerifyCommandName(commandName);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "commandName", ex);
			}
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			commandName = commandName.ToLower();
			lock (_listenersLock)
			{
				if (_listeners.ContainsKey(commandName))
				{
					HashSet<CommandListener> set = _listeners[commandName];
					set.Remove(listener);
					if (set.Count == 0)
					{
						_listeners.Remove(commandName);
					}
				}
			}
		}

		[Subscribe]
		public void OnCommand(CommandEvent command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			CommandListener[] array = null;
			lock (_listeners)
			{
				if (_listeners.ContainsKey(command.CommandName))
				{
					var count = _listeners[command.CommandName].Count;
					array = new CommandListener[count];
					_listeners[command.CommandName].CopyTo(array, 0);
				}
			}
			if (array != null)
			{
				foreach (CommandListener listener in array)
				{
					listener.OnCommand(command);
				}
			}
		}

		[Subscribe]
		public void OnRegisterPluginCommands(RegisterPluginCommandsRequest message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			RegisterAllCommandsForPlugin(message.Plugin);
		}

		private void RegisterAllCommandsForPlugin(Plugin plugin)
		{
			foreach(MethodInfo method in plugin.GetType().GetMethods())
			{
				CommandAttribute attr = method.GetCustomAttribute<CommandAttribute>();
				if (attr!=null)
				{
					AddListener(new PluginCommandListener(plugin, attr.CommandName, method));
				}
			}
		}

		private void AddListener(PluginCommandListener listener)
		{
			AddListener(listener.Command, listener);
		}

		private void UnregisterAllCommandsForPlugin(Plugin plugin)
		{
			List<PluginCommandListener> removalQueue = new List<PluginCommandListener>();
			foreach (CommandListener listener in _listeners.Values)
			{
				if (listener is PluginCommandListener)
				{
					PluginCommandListener pluginListener = (PluginCommandListener)listener;
					if (pluginListener.Plugin == plugin)
					{
						removalQueue.Add(pluginListener);
					}
				}
			}
			foreach(PluginCommandListener listener in removalQueue)
			{
				_listeners.Remove(listener.Command);
			}
		}

		private class PluginCommandListener : CommandListener
		{
			public MethodInfo CallbackMethod { get; }
			public Plugin Plugin { get; }
			public string Command { get; }

			public PluginCommandListener(Plugin plugin, string command, MethodInfo callback)
			{
				Plugin = plugin;
				CallbackMethod = callback;
				Command = command;
			}
			
			public void OnCommand(CommandEvent info)
			{
				CallbackMethod.Invoke(Plugin, new object[] { info });
			}
		}

	}
}