using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Core.Plugin
{
	public interface Plugin
	{
		PluginManager Manager { get; }

		void Load(IUnityContainer container);

		void Unload();
	}
}