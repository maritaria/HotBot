using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Gives a description of a plugin that can be presented to the user.
	/// </summary>
	public sealed class PluginDescription
	{
		/// <summary>
		/// Gets the name of the plugin.
		/// This string is only allowed to contain alphanumeric characters.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the descriptionIReflectionPluginDescription of the plugin.
		/// </summary>
		public string Description { get; }

		/// <summary>
		/// Gets the dependencies of the plugin.
		/// </summary>
		public IReadOnlyCollection<Dependency> Dependencies { get; }//TODO: Separate class for dependencies

		public PluginDescription(string name, string description, params Dependency[] dependencies)
		{
			try
			{
				VerifyName(name);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "name", ex);
			}
			try
			{
				VerifyDescription(description);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "description", ex);
			}
			Name = name;
			Description = description;
			Dependencies = dependencies;
		}

		public static void VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("Cannot be empty", "name");
			}
			if (!name.All(char.IsLetterOrDigit))
			{
				throw new ArgumentException("May only contain alphanumeric characters", "name");
			}
		}

		public static void VerifyDescription(string description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
		}
	}
}
