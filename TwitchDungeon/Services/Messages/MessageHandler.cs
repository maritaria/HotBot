using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Messages
{
	public interface MessageHandler<T>
	{
		void HandleMessage(T message);
	}
}
