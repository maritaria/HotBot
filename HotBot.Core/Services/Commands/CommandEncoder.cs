using System;
using System.Collections.ObjectModel;
using System.Linq;
using HotBot.Core.Services.Irc;
using HotBot.Core.Services.Util;

namespace HotBot.Core.Services.Commands
{
	public class CommandEncoder : MessageHandler<IrcMessageEnhanced>
	{
		public Collection<string> Prefixes { get; set; } = new Collection<string>();

		public MessageBus Bus { get; }

		public CommandEncoder(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			//TODO: Read prefixes from configuration
			Prefixes.Add("!");
			Bus = bus;
			Bus.Subscribe(this);
		}

		public void HandleMessage(IrcMessageEnhanced message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (ShouldDecode(message))
			{
				var commandInfo = Decode(message);
				Bus.Publish(commandInfo);
			}
		}

		private bool ShouldDecode(IrcMessageEnhanced message)
		{
			if (message.Message.Length > 0)
			{
				foreach (string prefix in Prefixes)
				{
					if (message.Message.StartsWith(prefix))
					{
						return true;
					}
				}
			}
			return false;
		}

		private CommandInfo Decode(IrcMessageEnhanced message)
		{
			string content = message.Message.Trim();
			content = RemovePrefix(content);
			string[] parts = content.SplitOnce(" ", "\t");
			string commandName = parts[0];
			string argumentText = parts[1];
			CommandInfo commandInfo = new CommandInfo(message.Channel, message.User, commandName, argumentText);
			return commandInfo;
		}

		private string RemovePrefix(string text)
		{
			foreach(string prefix in Prefixes)
			{
				if (text.StartsWith(prefix))
				{
					return text.Substring(prefix.Length);
				}
			}
			throw new InvalidOperationException("string does not start with known prefix");
		}
	}
}