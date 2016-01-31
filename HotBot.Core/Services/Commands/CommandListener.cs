using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Services.Commands
{
	public interface CommandListener
	{
		void OnCommand(CommandInfo info);
	}
}