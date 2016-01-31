using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugin
{
	public interface PluginManager
	{


		void AddPlugin(Plugin plugin);
		void RemovePlugin(Plugin plugin);
	}
}
