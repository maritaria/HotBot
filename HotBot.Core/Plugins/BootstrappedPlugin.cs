using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	[Obsolete]
	public interface BootstrappedPlugin : Plugin
	{
		void Bootstrap(IUnityContainer container);//TODO: Use UnityContainerExtensions instead
	}
}
