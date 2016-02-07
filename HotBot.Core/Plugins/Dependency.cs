namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Class representing the dependency a plugin has on another plugin.
	/// </summary>
	public class Dependency
	{
		/// <summary>
		/// The name of the plugin being depended upon
		/// </summary>
		public string PluginName { get; }
		/// <summary>
		/// The type of dependency exists between the two plugins.
		/// </summary>
		public DependencyType Type { get; }
	}
}