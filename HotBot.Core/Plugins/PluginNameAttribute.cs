using System;
using System.Linq;

namespace HotBot.Core.Plugins
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PluginNameAttribute : Attribute
	{
		public string Name { get; }

		public PluginNameAttribute(string name)
		{
			//TODO: Verify name
			ReflectionPluginDescription.VerifyName(name);
			Name = name;
		}
	}
}