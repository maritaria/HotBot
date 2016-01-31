using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Plugins
{
	public sealed class ReflectionPluginDescription : PluginDescription
	{
		public string Name { get; }

		public string Description { get; }

		public IReadOnlyCollection<Dependency> Dependencies { get; }

		public ReflectionPluginDescription(Plugin plugin)
		{
			//TODO: Use reflection to get the required information
		}
		//TODO: pick a better spot to put this
		public static void VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("cannot be empty string", "name");
			}
			if (!name.All(char.IsLetterOrDigit))
			{
				throw new ArgumentException("name can only contain alphanumeric characters");
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