using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace TwitchDungeon.Services.Commands
{
	public class CommandInitializer
	{
		private IUnityContainer _container;

		public CommandInitializer(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			var commandEncoder = _container.Resolve<CommandEncoder>();
		}
	}
}