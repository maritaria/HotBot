using System;
using System.Collections.ObjectModel;
using System.Linq;
using TwitchDungeon.Services.DataStorage;
using TwitchDungeon.Services.Irc;
using TwitchDungeon.Services.Messages;
using TwitchDungeon.Services.Util;

namespace TwitchDungeon.Services.Commands
{
	public class CommandEncoder : MessageHandler<IrcMessageEnhanced>
	{
		//TODO: Make prefixes strings
		public Collection<char> Prefixes { get; set; } = new Collection<char>();
		public MessageBus Bus { get; }

		public CommandEncoder(MessageBus bus)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			//TODO: Read prefixes from configuration
			Prefixes.Add('!');
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
			return message.Text.Length > 0 && Prefixes.Contains(message.Text.First());
		}

		private CommandInfo Decode(IrcMessageEnhanced message)
		{
			string content = message.Text.Trim();
			content = RemovePrefix(content);
			string[] parts = content.SplitOnce(" ", "\t");
			string commandName = parts[0];
			string argumentText = parts[1];
			CommandInfo commandInfo = new CommandInfo(message.Channel, message.User, commandName, argumentText);
			return commandInfo;
		}

		private string RemovePrefix(string text)
		{
			return text.Substring(1);
		}
	}
}