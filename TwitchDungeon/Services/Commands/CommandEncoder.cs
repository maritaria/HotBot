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
		public Collection<char> Prefixes { get; } = new Collection<char>();
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
			Bus.Subscribe<IrcMessageEnhanced>(this);
		}

		private void OnIrcMessageEnhanced(MessageBus bus, IrcMessageEnhanced enhanced)
		{
			if (ShouldDecode(enhanced))
			{
				var commandInfo = Decode(enhanced);
				Bus.Publish(commandInfo);
			}
		}

		public bool ShouldDecode(IrcMessageEnhanced message)
		{
			return message.Text.Length > 0 && Prefixes.Contains(message.Text.First());
		}

		public CommandInfo Decode(IrcMessageEnhanced message)
		{
			string content = message.Text.Trim();
			content = RemovePrefix(content);
			string[] parts = content.SplitOnce(" ", "\t");
			string commandName = parts[0];
			string argumentText = parts[1];
			CommandInfo commandInfo = new CommandInfo(message.User, commandName, argumentText);
			return commandInfo;
		}

		public string RemovePrefix(string text)
		{
			return text.Substring(1);
		}

		protected override void Handle(IrcMessageEnhanced item)
		{
			OnIrcMessageEnhanced(Bus, item);
		}
	}
}