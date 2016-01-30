using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Messages
{
	internal sealed class DictionaryMessageBus : MessageBus
	{
		private Dictionary<Type, List<MessageHandler>> _subscribers = new Dictionary<Type, List<MessageHandler>>();
		
		public DictionaryMessageBus()
		{
		}
		
		public void Subscribe<TData>(MessageBusAction<TData> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			Subscribe(new ActionHandler<TData>(callback));
		}

		public void Subscribe<TData>(MessageHandler<TData> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (handler.Bus != null)
			{
				throw new InvalidHandlerArgumentException("already assigned to a bus", "handler");
			}
			handler.Bus = this;
			if (!_subscribers.ContainsKey(typeof(TData)))
			{
				_subscribers.Add(typeof(TData), new List<MessageHandler>());
			}
			_subscribers[typeof(TData)].Add(handler);
		}

		public void Publish<TData>(TData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (HasSubscribers<TData>())
			{
				foreach (MessageHandler h in _subscribers[typeof(TData)])
				{
					h.Handle(data);
				}
			}
		}

		public bool HasSubscribers<TData>()
		{
			return _subscribers.ContainsKey(typeof(TData));
		}

		private class ActionHandler<T> : MessageHandler<T>
		{
			private readonly MessageBusAction<T> _handler;

			public ActionHandler(MessageBusAction<T> handler)
			{
				_handler = handler;
			}

			protected override void Handle(T item)
			{
				_handler(Bus, item);
			}
		}
	}
}