using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Messages
{
	public interface MessageBus
	{
		//TODO: Refactor away the MessageBus argument; it's useless
		void Subscribe<TData>(MessageHandler<TData> handler);
		bool IsSubscribed<TData>(MessageHandler<TData> handler);
		void Unsubscribe<TData>(MessageHandler<TData> handler);
		void Publish<TData>(TData data);
	}

}