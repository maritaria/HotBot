using System;

namespace HotBot.Core.Plugins
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class PluginDependencyAttribute : Attribute
	{
		public Type Dependency { get; }

		public PluginDependencyAttribute(Type dependency)
		{
			if (dependency == null)
			{
				throw new ArgumentNullException("dependency");
			}
			Dependency = dependency;
		}
	}
}