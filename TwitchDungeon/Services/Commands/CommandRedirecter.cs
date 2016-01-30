using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Commands
{
	public class CommandRedirecter : MessageHandler<CommandInfo>
	{
		private Dictionary<string, HashSet<CommandListener>> _handlers = new Dictionary<string, HashSet<CommandListener>>();
		private object _handlersLock = new object();
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

		public void AddHandler(string commandName, CommandListener listener)
		{
			CommandInfo.VerifyCommandName(commandName);
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			lock (_handlersLock)
			{
				if (!_handlers.ContainsKey(commandName))
				{
					var listeners = new HashSet<CommandListener>();
					listeners.Add(listener);
					_handlers[commandName] = listeners;
				}
				else
				{
					_handlers[commandName].Add(listener);
				}
			}
		}

		public void RemoveHandler(string commandName, CommandListener listener)
		{
			CommandInfo.VerifyCommandName(commandName);
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			lock (_handlersLock)
			{
				if (_handlers.ContainsKey(commandName))
				{
					HashSet<CommandListener> set = _handlers[commandName];
					set.Remove(listener);
					if (set.Count == 0)
					{
						_handlers.Remove(commandName);
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
			lock (_handlers)
			{
				if (_handlers.ContainsKey(command.CommandName))
				{
					var count = _handlers[command.CommandName].Count;
					array = new CommandListener[count];
					_handlers[command.CommandName].CopyTo(array, 0);
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