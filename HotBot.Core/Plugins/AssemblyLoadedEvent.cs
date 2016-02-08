using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	public sealed class AssemblyLoadedEvent
	{
		public Assembly Assembly { get; }
		
		public AssemblyLoadedEvent(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			Assembly = assembly;
		}
	}
}
