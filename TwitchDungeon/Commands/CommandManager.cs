using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchDungeon.Services;

namespace TwitchDungeon.Commands
{
	public class CommandManager
	{
		static CommandManager()
		{
			ServiceManager.Register(new CommandManager());
		}

		private CommandManager()
		{

		}

	}
}
