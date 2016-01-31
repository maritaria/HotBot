using System;
using System.Linq;
using HotBot.Core.DataStorage.Permissions;
using HotBot.Core.DataStorage;

namespace HotBot.Core.Commands
{
	public sealed class CommandInfo
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

		public CommandInfo(Channel channel, User sender, string commandName, string argumentText) : this(channel, sender, sender, commandName, argumentText)
		{
			//TODO: Expand unit tests
		}

		public CommandInfo(Channel channel, User sender, User authorizer, string commandName, string argumentText)
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
			VerifyCommandName(commandName);
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

		public static bool IsValidCommandName(string commandName)
		{
			try
			{
				VerifyCommandName(commandName);
			}
			catch(InvalidCommandNameException)
			{
				return false;
			}
			return true;
		}
		
		//TODO: Create new type to encapsulate commandname behaviour
		public static void VerifyCommandName(string commandName)
		{
			if (commandName == null)
			{
				throw new InvalidCommandNameException("Command name cannot be null");
			}
			if (commandName == string.Empty)
			{
				throw new InvalidCommandNameException("Command name cannot be empty");
			}
			if (commandName.Any(char.IsWhiteSpace))
			{
				throw new InvalidCommandNameException("Command name cannot contain whitespace");
			}
		}
	}
}