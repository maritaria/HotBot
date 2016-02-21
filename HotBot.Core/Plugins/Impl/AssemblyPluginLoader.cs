using HotBot.Core.Intercom;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Plugins.Impl
{
	internal sealed class AssemblyPluginLoader : PluginLoader
	{
		public const string PluginAssemblyNamePrefix = "HotBot.Plugins.";
		public const string PluginAssemblyNamePattern = PluginAssemblyNamePrefix + "*.dll";

		public string PluginDirectory { get; } = "Plugins";

		[Dependency]
		public IUnityContainer DependencyContainer { get; set; }

		[Dependency]
		public MessageBus Bus { get; set; }

		public AssemblyPluginLoader()
		{
			AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
		}

		public void LoadPlugins()
		{
			foreach (string assemblyFilename in FindPluginAssemblyFilenames())
			{
				LoadAssembly(assemblyFilename);
			}
		}

		private IEnumerable<string> FindPluginAssemblyFilenames()
		{
			return Directory.EnumerateFiles(PluginDirectory, PluginAssemblyNamePattern);
		}

		private Assembly LoadAssembly(string assemblyFilename)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFilename);
			return assembly;
		}

		private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			if (IsPluginAssembly(args.LoadedAssembly))
			{
				var plugin = CreatePluginFromAssembly(args.LoadedAssembly);
				PublishPlugin(plugin);
			}
		}

		private bool IsPluginAssembly(Assembly loadedAssembly)
		{
			return loadedAssembly.GetName().Name.StartsWith(PluginAssemblyNamePrefix);
		}

		private Plugin CreatePluginFromAssembly(Assembly assembly)
		{
			PluginAttribute attr = assembly.GetCustomAttribute<PluginAttribute>();
			if (attr == null)
			{
				throw new ArgumentException($"Assembly '{assembly.FullName}' doesn't have a PluginAssemblyAttribute", "assembly");
			}
			DependencyContainer.RegisterType(attr.PluginClass, new ContainerControlledLifetimeManager());
			return (Plugin)DependencyContainer.Resolve(attr.PluginClass);
		}

		private void PublishPlugin(Plugin plugin)
		{
			Bus.Publish(new PluginCreatedEvent(plugin));
		}
	}
}