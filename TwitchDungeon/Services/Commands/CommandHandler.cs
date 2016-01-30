using System;
using System.Linq;

namespace TwitchDungeon.Services.Commands
{
	public interface CommandHandler
	{
		//TODO: use Task<bool>  + await Handle();
		void Handle(CommandInfo e);
	}
}