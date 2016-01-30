using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Commands
{
	public class CommandRedirecter : MessageHandler<CommandInfo>
	{
		private Dictionary<string, CommandHandler> _handlers = new Dictionary<string, CommandHandler>();
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

		public void HandleMessage(CommandInfo message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
		}
	}
}