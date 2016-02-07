using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Commands
{
	public interface CommandConfig//TODO: better name
	{
		IEnumerable<string> Prefixes { get; set; }
	}
}
