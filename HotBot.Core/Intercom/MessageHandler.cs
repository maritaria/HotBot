using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	[Obsolete("Use attributes instead")]
	public interface MessageHandler<TEvent>
	{
		void HandleMessage(TEvent message);
	}
}