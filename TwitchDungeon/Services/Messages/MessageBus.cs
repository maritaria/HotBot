using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Messages
{
	public interface MessageBus
	{
		//TODO: Refactor away the MessageBus argument; it's useless
		void Subscribe<TData>(MessageBusAction<TData> handler);
		void Subscribe<TData>(MessageHandler<TData> handler);
		void Publish<TData>(TData data);

	}

	public delegate void MessageBusAction<TData>(MessageBus bus, TData data);

}