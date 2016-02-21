using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class SubscribeAttribute : Attribute
	{
		public Type PublishedType { get; }

		public SubscribeAttribute()
		{
		}

		public SubscribeAttribute(Type publishedType)
		{
			if (publishedType == null)
			{
				throw new ArgumentNullException("publishedType");
			}
			PublishedType = publishedType;
		}
	}
}