using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	public interface CommandManager
	{
		/// <summary>
		/// Registers a command handler so it receives callbacks when a command is run.
		/// All methods decorated with a <see cref="CommandAttribute"/> will be registered.
		/// </summary>
		/// <param name="commandHandler">The object to register</param>
		void Register(object commandHandler);

		/// <summary>
		/// Unregisters a command handler so it no longer receives callbacks for commands.
		/// </summary>
		/// <param name="commandHandler">The object to unregister</param>
		void Unregister(object commandHandler);

		/// <summary>
		/// Checks whether a given object has any callbacks registered with the <see cref="CommandManager"/>.
		/// </summary>
		/// <param name="commandHandler">The object to check registration for</param>
		/// <returns>True if the object has callbacks registered with the manager, false otherwise.</returns>
		bool IsRegistered(object commandHandler);

		/// <summary>
		/// Runs a command so it gets propagated to all handlers that can handle the command.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		void RunCommand(CommandEvent command);
	}
}