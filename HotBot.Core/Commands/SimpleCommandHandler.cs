using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Commands
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
			Redirecter.AddListener("test", this);
		}

		public void OnCommand(CommandEvent info)
		{
			Console.WriteLine($"SimpleCommandHandler.OnCommand({info.CommandName})");
		}
	}
}
