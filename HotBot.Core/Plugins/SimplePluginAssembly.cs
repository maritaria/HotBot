using System;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Plugins
{
	internal sealed class SimplePluginAssembly : PluginAssembly
	{
		public string AssemblyLocation { get; private set; }

		public bool IsLoaded { get; private set; }

		public Assembly LoadedAssembly { get; private set; }

		public void Load()
		{
			if (IsLoaded)
			{
				throw new InvalidOperationException("PluginAssembly already loaded");
			}
			LoadedAssembly = Assembly.LoadFrom(AssemblyLocation);
		}

		public void Unload()
		{
			//Cannot unload assembly, only by unloading an AppDomain
			throw new NotSupportedException();
		}
	}
}