using HotBot.Core.Plugins;
using System;
using System.Linq;

namespace HotBot.Core.Commands
{
	[Obsolete]
	public sealed class RegisterPluginCommandsRequest
	{
		public Plugin Plugin { get; }

		public RegisterPluginCommandsRequest(Plugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			Plugin = plugin;
		}
	}
}