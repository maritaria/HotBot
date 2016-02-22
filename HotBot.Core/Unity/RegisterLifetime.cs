using HotBot.Core.Util;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot.Core.Unity
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RegisterLifetime : Attribute
	{
		public Type LifetimeManagerType { get; }

		public RegisterLifetime(Type managerType)
		{
			Verify.NotNull(managerType, "managerType");
			LifetimeManagerType = managerType;
		}

		public LifetimeManager CreateManager()
		{
			return (LifetimeManager)Activator.CreateInstance(LifetimeManagerType);
		}
	}
}