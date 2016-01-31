using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core
{
	public interface MessageBus
	{
		void Subscribe<TData>(MessageHandler<TData> handler);
		bool IsSubscribed<TData>(MessageHandler<TData> handler);
		void Unsubscribe<TData>(MessageHandler<TData> handler);
		void Publish<TData>(TData data);
	}

}