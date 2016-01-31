namespace HotBot.Core.Plugins
{
	/// <summary>
	/// Declares the different types of depencencies a plugin can have on other plugins.
	/// These are only relevant to solving dependencies in the load order or plugins.
	/// </summary>
	public enum DependencyType
	{
		/// <summary>
		/// The dependency must be loaded before the dependant plugin loads.
		/// If the dependency is missing the dependant plugin cannot be loaded.
		/// </summary>
		Required,
		/// <summary>
		/// The dependency must be loaded before the dependant plugin loads.
		/// If the dependency is missing the dependant plugin can still be loaded.
		/// </summary>
		Optional,
		/// <summary>
		/// The dependant plugin wants to be notified when the dependency is loaded.
		/// The notification will always happen after load, but doesn't guarentee that the dependency will ever load.
		/// A circular dependency will only be solvable by a DependencyType.Notify in the dependency chain.
		/// </summary>
		Notify,
	}
}