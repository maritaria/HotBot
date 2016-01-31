using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Irc
{
	public class PipelineInitializer
	{
		private IUnityContainer _container;

		public PipelineInitializer(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.Resolve<PrivateMessageDecoder>();
			_container.Resolve<IrcLogger>();
		}

	}
}
