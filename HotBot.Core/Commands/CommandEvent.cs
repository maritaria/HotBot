using HotBot.Core.Irc;
using HotBot.Core.Permissions;
using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public sealed class CommandEvent
	{
		/// <summary>
		/// Gets the user who is the main focus of the command
		/// </summary>
		public User User { get; }

		/// <summary>
		/// The channel the command is being executed in
		/// </summary>
		public ChannelData Channel { get; }

		/// <summary>
		/// Gets or sets the entity holding the permissions available for the command
		/// </summary>
		public Authorizer Authorizer { get; set; }

		/// <summary>
		/// The name of the command
		/// </summary>
		public string CommandName { get; }

		/// <summary>
		/// The string containing the arguments for the command
		/// </summary>
		public string ArgumentText { get; }

		public CommandEvent(ChannelData channel, User sender, string commandName, string argumentText) : this(channel, sender, sender, commandName, argumentText)
		{
		}

		public CommandEvent(ChannelData channel, User sender, User authorizer, string commandName, string argumentText)
		{
			Verify.NotNull(channel, "channel");
			Verify.NotNull(sender, "sender");
			Verify.NotNull(authorizer, "authorizer");
			Verify.CommandName(commandName, "commandName");
			Verify.NotNull(argumentText, "argumentText");

			Channel = channel;
			User = sender;
			Authorizer = authorizer;
			CommandName = commandName;
			ArgumentText = argumentText;
		}
	}
}