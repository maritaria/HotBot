using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchDungeon.Services.Messages
{
	public sealed class MessageBus
	{
		private Dictionary<Type, List<Handler>> _subscribers = new Dictionary<Type, List<Handler>>();
		
		private MessageBus()
		{
		}

		public void Subscribe<TData>(Action<MessageBus, TData> handler)
		{
			Subscribe(new ActionHandler<TData>(handler));
		}

		public void Subscribe<TData>(Handler<TData> handler)
		{
			if (handler.Bus != null)
			{
				throw new ArgumentException("already assigned to a bus", "handler");
			}
			handler.Bus = this;
			if (!_subscribers.ContainsKey(typeof(TData)))
			{
				_subscribers.Add(typeof(TData), new List<Handler>());
			}
			_subscribers[typeof(TData)].Add(handler);
		}

		public void Publish<TData>(TData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			foreach (Handler h in _subscribers[typeof(TData)])
			{
				h.Handle(data);
			}
		}
		
		private class ActionHandler<T> : Handler<T>
		{
			private readonly Action<MessageBus, T> _handler;

			public ActionHandler(Action<MessageBus, T> handler)
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