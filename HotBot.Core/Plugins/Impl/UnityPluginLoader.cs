using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBot.Core.Intercom;
using Microsoft.Practices.Unity;
using System.Reflection;
using System.IO;
using HotBot.Core.Unity;

namespace HotBot.Core.Plugins.Impl
{
	//[RegisterFor(typeof(PluginLoader))]
	public class UnityPluginLoader : RegistrationConvention, PluginLoader
	{
		public const string PluginAssemblyNamePrefix = "HotBot.Plugins.";
		public const string PluginAssemblyNamePattern = PluginAssemblyNamePrefix + "*.dll";
		public string PluginDirectory { get; } = "Plugins";

		[Dependency]
		public MessageBus Bus { get; set; }

		[Dependency]
		public IUnityContainer DependencyInjector { get; set; }

		[Dependency]
		public PluginManager Manager { get; set; }

		public void LoadPlugins()
		{
			DependencyInjector.RegisterTypes(this, false);
		}

		public override Func<Type, IEnumerable<Type>> GetFromTypes()
		{
			return HotBotRegistrationConvention.GetFromTypes;
		}

		public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers()
		{
			return null;
		}

		public override Func<Type, LifetimeManager> GetLifetimeManager()
		{
			return HotBotRegistrationConvention.GetLifetimeManager;
		}

		public override Func<Type, string> GetName()
		{
			return HotBotRegistrationConvention.GetName;
		}

		public override IEnumerable<Type> GetTypes()
		{
			return HotBotRegistrationConvention.GetTypes(GetPluginAssemblies());
		}

		private IEnumerable<Assembly> GetPluginAssemblies()
		{
			foreach (string assemblyFile in FindPluginAssemblies())
			{
				yield return LoadAssembly(assemblyFile);
			}
		}

		private Assembly LoadAssembly(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile);
		}

		private IEnumerable<string> FindPluginAssemblies()
		{
			return Directory.EnumerateFiles(PluginDirectory, PluginAssemblyNamePattern);
		}
	}
}
