using System;
using System.Linq;

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

		public PluginDescription(string name, string description)
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