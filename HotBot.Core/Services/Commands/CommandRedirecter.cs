using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Services.Commands
{
	public class CommandRedirecter : MessageHandler<CommandInfo>
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
			CommandInfo.VerifyCommandName(commandName);
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
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
			CommandInfo.VerifyCommandName(commandName);
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
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

		public void ExecuteCommand(CommandInfo command)
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

		void MessageHandler<CommandInfo>.HandleMessage(CommandInfo message)
		{
			ExecuteCommand(message);
		}
	}
}