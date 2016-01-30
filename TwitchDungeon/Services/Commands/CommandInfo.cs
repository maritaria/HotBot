using System;
using System.Linq;
using TwitchDungeon.DataStorage.Permissions;

namespace TwitchDungeon.Services.Commands
{
	public sealed class CommandInfo
	{
		/// <summary>
		/// Get's the user who is the main focus of the command
		/// </summary>
		public User Sender { get; }

		/// <summary>
		/// Get's the entity holding the permissions available for the command
		/// </summary>
		public Authorizer Authorizer { get; }
		
		/// <summary>
		/// The name of the command
		/// </summary>
		public string CommandName { get; }

		/// <summary>
		/// The string containing the arguments for the command
		/// </summary>
		public string ArgumentText { get; }


		public CommandInfo(User sender, string commandName, string argumentText) : this(sender, sender, commandName, argumentText)
		{

		}

		public CommandInfo(User sender, User authorizer, string commandName, string argumentText)
		{
			Sender = sender;
			Authorizer = authorizer;
			CommandName = commandName;
			ArgumentText = argumentText;
		}
	}
}