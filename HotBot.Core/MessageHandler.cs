using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core
{
	public interface MessageHandler<TData>
	{
		void HandleMessage(TData message);
	}
}
