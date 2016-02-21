using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Intercom
{
	public interface Publisher
	{
		MessageBus Bus { get; }
	}
}
