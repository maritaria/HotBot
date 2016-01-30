using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Commands
{
	public abstract class SimpleCommandHandler : CommandHandler
	{
		public void Handle(CommandInfo e)
		{
		}

		protected void ExecuteCore(CommandInfo e)
		{
		}
	}
}
