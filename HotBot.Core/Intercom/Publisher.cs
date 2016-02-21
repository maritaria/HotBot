using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	public interface Publisher
	{
		MessageBus Bus { get; }
	}
}