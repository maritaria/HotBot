using HotBot.Core.Intercom;
using HotBot.Core.Unity;
using HotBot.Core.Util;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Commands
{
	[RegisterFor(typeof(CommandManager))]
	[RegisterLifetime(typeof(ContainerControlledLifetimeManager))]
	public sealed class ReflectionCommandManager : CommandManager
	{
		private Dictionary<string, Dictionary<object, MethodInfo>> _handlers = new Dictionary<string, Dictionary<object, MethodInfo>>();

		public MessageBus Bus { get; }

		public ReflectionCommandManager(MessageBus bus)
		{
			Verify.NotNull(bus, "bus");
			Bus = bus;
			Bus.Subscribe(this);
		}

		public void Register(object commandHandler)
		{
			Verify.NotNull(commandHandler, "commandHandler");
			foreach (MethodInfo handlerMethod in commandHandler.GetType().GetMethods())
			{
				Register(commandHandler, handlerMethod);
			}
		}

		private void Register(object commandHandler, MethodInfo handlerMethod)
		{
			foreach (CommandAttribute attr in handlerMethod.GetCustomAttributes<CommandAttribute>())
			{
				Subscribe(commandHandler, handlerMethod, attr);
			}
		}

		private void Subscribe(object commandHandler, MethodInfo handlerMethod, CommandAttribute attr)
		{
			string commandName = attr.CommandName;
			var handlers = EnsureHandlers(commandName);
			if (!handlers.ContainsKey(commandHandler))
			{
				handlers.Add(commandHandler, handlerMethod);
			}
		}

		private Dictionary<object, MethodInfo> EnsureHandlers(string commandName)
		{
			if (!_handlers.ContainsKey(commandName))
			{
				_handlers.Add(commandName, new Dictionary<object, MethodInfo>());
			}
			return _handlers[commandName];
		}

		public void Unregister(object commandHandler)
		{
			Verify.NotNull(commandHandler, "commandHandler");
			List<string> emptyHandlerDictionaries = new List<string>();
			foreach (KeyValuePair<string, Dictionary<object, MethodInfo>> pairs in _handlers)
			{
				var commandName = pairs.Key;
				var subs = pairs.Value;
				subs.Remove(commandHandler);
				if (subs.Count == 0)
				{
					emptyHandlerDictionaries.Add(commandName);
				}
			}
			foreach (string commandName in emptyHandlerDictionaries)
			{
				_handlers.Remove(commandName);
			}
		}

		public bool IsRegistered(object commandHandler)
		{
			Verify.NotNull(commandHandler, "commandHandler");
			return _handlers.Any(d => d.Value.ContainsKey(commandHandler));
		}

		[Subscribe]
		public void RunCommand(CommandEvent command)
		{
			Verify.NotNull(command, "command");
			if (!_handlers.ContainsKey(command.CommandName))
			{
				return;
			}
			foreach (KeyValuePair<object, MethodInfo> handler in _handlers[command.CommandName])
			{
				var owner = handler.Key;
				var function = handler.Value;
				function.Invoke(owner, new object[] { command });
			}
		}
	}
}