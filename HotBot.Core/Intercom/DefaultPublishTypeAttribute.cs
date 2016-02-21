using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	/// <summary>
	/// Sets the default type by which the type will be published when passed to <see cref="MessageBus.PublishSpecific{TEvent}(TEvent)"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
	public sealed class DefaultPublishTypeAttribute : Attribute
	{
		public Type PublishType { get; }

		public DefaultPublishTypeAttribute(Type defaultPublishType)
		{
			if (defaultPublishType == null)
			{
				throw new ArgumentNullException("defaultPublishType");
			}
			PublishType = defaultPublishType;
		}
	}
}