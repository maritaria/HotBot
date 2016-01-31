using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Commands
{
	public interface CommandListener
	{
		void OnCommand(CommandEvent info);
	}
}