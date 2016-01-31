using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	public interface PluginManager
	{
		PluginManagerState State { get; }

		void AddPlugin(Plugin plugin);
		void RemovePlugin(Plugin plugin);

		Plugin GetPlugin(string name);
		Plugin GetPlugin(Type type);
		TPlugin GetPlugin<TPlugin>() where TPlugin : Plugin;

		void Startup();
		void Shutdown();
		void Restart();
	}
}
