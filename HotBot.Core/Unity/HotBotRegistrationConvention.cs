using HotBot.Core.Util;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Unity
{
	public sealed class HotBotRegistrationConvention : RegistrationConvention
	{
		public Assembly Target { get; }

		public HotBotRegistrationConvention(Assembly target)
		{
			Verify.NotNull(target, "target");
			Target = target;
		}

		public override Func<Type, IEnumerable<Type>> GetFromTypes()
		{
			return GetFromTypes;
		}

		public static IEnumerable<Type> GetFromTypes(Type type)
		{
			return type.GetCustomAttributes<RegisterForAttribute>().Select(attr => attr.TargetType);
		}

		public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers()
		{
			return null;
		}

		public override Func<Type, LifetimeManager> GetLifetimeManager()
		{
			return GetLifetimeManager;
		}

		public static LifetimeManager GetLifetimeManager(Type type)
		{
			return type.GetCustomAttribute<RegisterLifetime>()?.CreateManager();
		}

		public override Func<Type, string> GetName()
		{
			return GetName;
		}

		public static string GetName(Type type)
		{
			return type.GetCustomAttribute<RegisterNameAttribute>()?.Name;
		}

		public override IEnumerable<Type> GetTypes()
		{
			return GetTypes(new Assembly[] { Assembly.GetExecutingAssembly() });
		}

		public static IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
		{
			return assemblies.SelectMany(asm => asm.GetTypes()).Where(CanRegisterType);
		}

		public static bool CanRegisterType(Type arg)
		{
			return arg.GetCustomAttributes<RegisterForAttribute>().Any();
		}
	}
}