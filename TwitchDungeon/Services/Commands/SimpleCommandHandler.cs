using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Commands
{
	public class SimpleCommandHandler : CommandListener
	{
		public CommandRedirecter Redirecter { get; }
		public SimpleCommandHandler(CommandRedirecter redirecter)
		{
			if (redirecter == null)
			{
				throw new ArgumentNullException("redirecter");
			}
			Redirecter = redirecter;
			Redirecter.AddHandler("test", this);
		}

		public void OnCommand(CommandInfo info)
		{
			Console.WriteLine($"SimpleCommandHandler.OnCommand({info.CommandName})");
		}
	}
}
