using System;

namespace HotBot.Core.Plugins
{
	[Serializable]
	public sealed class PluginLoadException : Exception
	{
		public string PluginName { get; }

		public PluginLoadException(string pluginName, string message) : base(message)
		{
			if (pluginName == null)
			{
				throw new ArgumentNullException("pluginName");
			}
			PluginName = pluginName;
		}

		public PluginLoadException(string pluginName, string message, Exception innerException) : base(message, innerException)
		{
			if (pluginName == null)
			{
				throw new ArgumentNullException("pluginName");
			}
			PluginName = pluginName;
		}
	}
}