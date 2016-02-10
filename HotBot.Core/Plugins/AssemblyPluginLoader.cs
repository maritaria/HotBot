using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HotBot.Core.Plugins
{
	internal sealed class AssemblyPluginLoader : PluginLoader
	{
		public const string PluginAssemblyNamePattern = "HotBot.Plugins.*.dll";

		public string PluginDirectory { get; } = "Plugins";

		[Dependency]
		public IUnityContainer DependencyContainer { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		public AssemblyPluginLoader()
		{
		}

		public IEnumerable<Plugin> LoadPlugins()
		{
			foreach (string assemblyFilename in FindPluginAssemblyFilenames())
			{
				Plugin plugin = null;
				try
				{
					Assembly asm = LoadAssembly(assemblyFilename);
					plugin = CreatePluginFromAssembly(asm);
				}
				catch (PluginLoadException ex)
				{
					Bus.PublishSpecific(ex);
					plugin = null;
				}
				catch (Exception ex)
				{
					Bus.PublishSpecific(new PluginLoadException(assemblyFilename, ex.Message, ex));
					plugin = null;
				}
				if (plugin != null)
				{
					yield return plugin;
				}
			}
		}

		private Assembly LoadAssembly(string assemblyFilename)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFilename);
			return assembly;
		}

		private Plugin CreatePluginFromAssembly(Assembly assembly)
		{
			AssemblyPluginAttribute attr = assembly.GetCustomAttribute<AssemblyPluginAttribute>();
			if (attr == null)
			{
				throw new ArgumentException($"Assembly '{assembly.FullName}' doesn't have a PluginAssemblyAttribute", "assembly");
			}
			DependencyContainer.RegisterType(attr.PluginClass, new ContainerControlledLifetimeManager());
			return (Plugin)DependencyContainer.Resolve(attr.PluginClass);
		}

		private IEnumerable<string> FindPluginAssemblyFilenames()
		{
			return Directory.EnumerateFiles(PluginDirectory, PluginAssemblyNamePattern);
		}

		public string GetPluginNameFromAssemblyFilename(string assemblyFilename)
		{
			var regex = new Regex(@"HotBot\.Plugins\.([a-zA-Z]+)\.dll");
			var match = regex.Match(assemblyFilename);
			return match.Captures[0].Value;
		}
	}
}