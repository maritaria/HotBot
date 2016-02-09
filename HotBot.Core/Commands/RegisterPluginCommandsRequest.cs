using HotBot.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Commands
{
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
