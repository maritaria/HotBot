using HotBot.Core.Commands;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public sealed class PluginCommandAttribute : Attribute
	{
		public string CommandName { get; }

		public PluginCommandAttribute(string commandName)
		{
			try
			{
				CommandEvent.VerifyCommandName(commandName);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "commandName", ex);
			}
			CommandName = commandName;
		}
	}
}