using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Plugins
{
	public interface AssemblyLoader
	{
		/// <summary>
		/// Gets the assemblies that have been loaded by the assembly loader
		/// </summary>
		IReadOnlyCollection<Assembly> LoadedAssemblies { get; }

		void LoadAssembly(AssemblyName assemblyName);
	}
}