using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	public class PluginLoader
	{
		private AssemblyLoader AssemblyLoader { get; }

		public string PluginDirectory { get; }

		public PluginLoader(AssemblyLoader assemblyLoader)
		{
			if (assemblyLoader == null)
			{
				throw new ArgumentNullException("assemblyLoader");
			}
			AssemblyLoader = assemblyLoader;
		}

		public void LoadPlugins()
		{
			foreach(PluginAssembly assembly in FindPluginAssemblies())
			{

			}
		}

		private IEnumerable<PluginAssembly> FindPluginAssemblies()
		{
			return null;
		}
	}
}
