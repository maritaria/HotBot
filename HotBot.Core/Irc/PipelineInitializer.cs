using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc
{
	public class PipelineInitializer
	{
		//TODO: Get rid of this
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
