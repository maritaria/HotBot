﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Plugins
{
	public sealed class SimpleAssemblyLoader : AssemblyLoader
	{
		private List<Assembly> _loadedAssemblies = new List<Assembly>();
		public IReadOnlyCollection<Assembly> LoadedAssemblies { get; }

		public SimpleAssemblyLoader()
		{
			LoadedAssemblies = new ReadOnlyCollection<Assembly>(_loadedAssemblies);
		}

		public void LoadAssembly(AssemblyName assemblyName)
		{
			Assembly asm = Assembly.Load(assemblyName);
			AddAssembly(asm);
		}

		private void AddAssembly(Assembly asm)
		{
			_loadedAssemblies.Add(asm);
		}
	}
}