using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services
{
	public class ServiceManager
	{
		private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();
		private static object _instancesLock = new object();

		public static TService GetInstance<TService>()
		{
			lock (_instancesLock)
			{
				return (TService)_instances[typeof(TService)];
			}
		}

		public static void Register<TService>() where TService : new()
		{
			Register<TService, TService>();
		}

		public static void Register<TService, TInstance>() where TInstance : TService, new()
		{
			Register<TService, TInstance>(new TInstance());
		}

		public static void Register<TService>(TService instance)
		{
			Register<TService, TService>(instance);
		}

		public static void Register<TService, TInstance>(TInstance instance)
		{
			lock (_instancesLock)
			{
				Type serviceType = typeof(TService);
				if (!serviceType.IsAssignableFrom(typeof(TInstance)))
				{
					throw new ArgumentException("is not assignable to the service type", "instance");
				}
				_instances[serviceType] = instance;
			}
		}



	}
}
