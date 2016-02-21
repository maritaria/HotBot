using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Commands
{
	public interface CommandManagerConfig
	{
		IEnumerable<string> Prefixes { get; set; }
	}
}