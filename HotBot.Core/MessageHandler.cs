using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core
{
	public interface MessageHandler<TEvent>
	{
		void HandleMessage(TEvent message);
	}
}
