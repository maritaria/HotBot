using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Defines a type that can hold information about a plugin.
	/// </summary>
	public interface PluginDescription
	{
		/// <summary>
		/// Gets the name of the plugin.
		/// This string is only allowed to contain alphanumeric characters.
		/// On implementations this property must be immutable.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the descriptionIReflectionPluginDescription of the plugin.
		/// On implementations this property must be immutable.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the dependencies of the plugin.
		/// On implementations this property must be immutable.
		/// </summary>
		IReadOnlyCollection<Dependency> Dependencies { get; }
	}
}
