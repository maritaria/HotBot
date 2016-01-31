using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core
{
	public interface MessageBus
	{
		void Subscribe<TEvent>(MessageHandler<TEvent> handler);
		bool IsSubscribed<TEvent>(MessageHandler<TEvent> handler);
		void Unsubscribe<TEvent>(MessageHandler<TEvent> handler);
		void Publish<TEvent>(TEvent data);
	}

}