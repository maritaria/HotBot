using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	public interface PluginAssembly
	{
		string AssemblyLocation { get; }
		bool IsLoaded { get; }
		void Load();
		void Unload();
	}
}
