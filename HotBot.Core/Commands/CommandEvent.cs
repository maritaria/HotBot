using HotBot.Core.Irc;
using HotBot.Core.Permissions;
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
		public Channel Channel { get; }

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

		public CommandEvent(Channel channel, User sender, string commandName, string argumentText) : this(channel, sender, sender, commandName, argumentText)
		{
		}

		public CommandEvent(Channel channel, User sender, User authorizer, string commandName, string argumentText)
		{
			if (channel == null)
			{
				throw new ArgumentNullException("channel");
			}
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (authorizer == null)
			{
				throw new ArgumentNullException("authorizer");
			}
			try
			{
				VerifyCommandName(commandName);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "commandName", ex);
			}
			if (argumentText == null)
			{
				throw new ArgumentNullException("argumentText");
			}

			Channel = channel;
			User = sender;
			Authorizer = authorizer;
			CommandName = commandName;
			ArgumentText = argumentText;
		}

		public static void VerifyCommandName(string commandName)
		{
			if (commandName == null)
			{
				throw new ArgumentNullException("commandName");
			}
			if (commandName == string.Empty)
			{
				throw new ArgumentException("cannot be empty", "commandName");
			}
			if (commandName.Any(char.IsWhiteSpace))
			{
				throw new ArgumentException("cannot contain whitespace(s)", "commandName");
			}
		}
	}
}