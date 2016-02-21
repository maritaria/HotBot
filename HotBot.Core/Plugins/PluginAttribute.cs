using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Attribute attached to an assembly to indicate the main plugin type.
	/// The type being indicated must implement <see cref="HotBot.Core.Plugins.Plugin"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class PluginAttribute : Attribute
	{
		public Type PluginClass { get; }

		public PluginAttribute(Type pluginClass)
		{
			if (pluginClass == null)
			{
				throw new ArgumentNullException("pluginClass");
			}
			if (!pluginClass.IsClass)
			{
				throw new ArgumentException("Type must be a class", "pluginClass");
			}
			if (pluginClass.IsAbstract)
			{
				throw new ArgumentException("Type cannot be abstract", "pluginClass");
			}
			if (pluginClass.IsArray)
			{
				throw new ArgumentException("Type cannot be an array", "pluginClass");
			}
			if (!pluginClass.GetInterfaces().Contains(typeof(Plugin)))
			{
				throw new ArgumentException($"Type does not implement {typeof(Plugin).FullName} interface");
			}
			PluginClass = pluginClass;
		}
	}
}
