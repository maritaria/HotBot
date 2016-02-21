using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public sealed class CommandAttribute : Attribute
	{
		public string CommandName { get; }

		public CommandAttribute(string commandName)
		{
			Verify.CommandName(commandName, "commandName");
			CommandName = commandName;
		}
	}
}