using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Commands
{
	public interface CommandListener
	{
		void OnCommand(CommandInfo info);
	}
}