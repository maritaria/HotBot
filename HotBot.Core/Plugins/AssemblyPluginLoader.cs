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
		public const string PluginAssemblyNamePattern = "HotBot.Plugin.*.dll";

		public string PluginDirectory { get; } = "Plugins";
		public IUnityContainer DependencyContainer { get; }
		public MessageBus Bus { get; }

		public AssemblyPluginLoader(IUnityContainer dependencyContainer, MessageBus bus)
		{
			if (dependencyContainer == null)
			{
				throw new ArgumentNullException("dependencyContainer");
			}
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			DependencyContainer = dependencyContainer;
			Bus = bus;
		}

		public IEnumerable<LoadablePlugin> LoadPlugins()
		{
			foreach (string assemblyFilename in FindPluginAssemblyFilenames())
			{
				LoadablePlugin plugin = null;
				try
				{
					Assembly asm = LoadAssembly(assemblyFilename);
					plugin = CreatePluginFromAssembly(asm);
					BootstrapPlugin(plugin);
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

		private void BootstrapPlugin(LoadablePlugin plugin)
		{
			if (plugin is BootstrappedPlugin)
			{
				BootstrappedPlugin bootstrappedPlugin = (BootstrappedPlugin)plugin;
				bootstrappedPlugin.Bootstrap(DependencyContainer);
			}
		}

		private Assembly LoadAssembly(string assemblyFilename)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFilename);
			return assembly;
		}

		private LoadablePlugin CreatePluginFromAssembly(Assembly assembly)
		{
			AssemblyPluginAttribute attr = assembly.GetCustomAttribute<AssemblyPluginAttribute>();
			if (attr == null)
			{
				throw new ArgumentException($"Assembly '{assembly.FullName}' doesn't have a PluginAssemblyAttribute", "assembly");
			}
			DependencyContainer.RegisterType(attr.PluginClass, new ContainerControlledLifetimeManager());
			return (LoadablePlugin)DependencyContainer.Resolve(attr.PluginClass);
		}

		private IEnumerable<string> FindPluginAssemblyFilenames()
		{
			return Directory.EnumerateFiles(PluginDirectory, PluginAssemblyNamePattern);
		}

		public string GetPluginNameFromAssemblyFilename(string assemblyFilename)
		{
			var regex = new Regex(@"HotBot\.Plugin\.([a-zA-Z]+)\.dll");
			var match = regex.Match(assemblyFilename);
			return match.Captures[0].Value;
		}
	}
}