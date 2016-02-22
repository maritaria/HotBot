using HotBot.Core.Util;
using System;
using System.Linq;

namespace HotBot.Core.Unity
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RegisterForAttribute : Attribute
	{
		public Type TargetType { get; }

		public RegisterForAttribute(Type targetType)
		{
			Verify.NotNull(targetType, "targetType");
			TargetType = targetType;
		}
	}
}