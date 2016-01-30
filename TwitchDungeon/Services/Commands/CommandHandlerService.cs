using System;
using System.Collections.ObjectModel;
using System.Linq;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Irc;

namespace TwitchDungeon.Services.Commands
{
	public class CommandHandlerService
	{
		private object _handlersLock = new object();
		public Collection<char> Prefixes { get; } = new Collection<char>();
		private Collection<CommandHandler> _handlers = new Collection<CommandHandler>();
		public ReadOnlyCollection<CommandHandler> Handlers { get; }

		private CommandHandlerService(DataService dataService)
		{
			Handlers = new ReadOnlyCollection<CommandHandler>(_handlers);
			Prefixes.Add('!');
		}

		public void AddHandler(CommandHandler handler)
		{
			lock (_handlersLock)
			{
				_handlers.Add(handler);
			}
		}

		public void RemoveHandler(CommandHandler handler)
		{
			lock (_handlersLock)
			{
				_handlers.Remove(handler);
			}
		}

		internal void TryHandle(ChatMessage message)
		{
			if (ShouldHandle(message))
			{
				Handle(message);
			}
		}

		private bool ShouldHandle(ChatMessage message)
		{
			return message.Message.Length > 0 && Prefixes.Contains(message.Message.First());
		}

		private void Handle(ChatMessage message)
		{
			CommandInfo info = new CommandInfo(message.User, message.Message);
			foreach(CommandHandler handler in Handlers)
			{
				handler.Handle(info);
			}
		}
	}
}