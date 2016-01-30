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
		/// Get's the unparsed command string
		/// </summary>
		public string CommandText { get; }

		public CommandInfo(User sender, string commandText) : this(sender, sender, commandText)
		{
		}

		public CommandInfo(User sender, User authorizer, string commandText)
		{
			Sender = sender;
			Authorizer = authorizer;
			CommandText = commandText;
		}
	}
}