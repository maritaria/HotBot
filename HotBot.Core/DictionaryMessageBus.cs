using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core
{
	public sealed class DictionaryMessageBus : MessageBus
	{
		private Dictionary<Type, HashSet<object>> _subscribers = new Dictionary<Type, HashSet<object>>();

		public DictionaryMessageBus()
		{
		}

		public void Subscribe<TEvent>(MessageHandler<TEvent> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!_subscribers.ContainsKey(typeof(TEvent)))
			{
				_subscribers.Add(typeof(TEvent), new HashSet<object>());
			}
			HashSet<object> subs = _subscribers[typeof(TEvent)];
			subs.Add(handler);
		}

		public bool IsSubscribed<TEvent>(MessageHandler<TEvent> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!_subscribers.ContainsKey(typeof(TEvent)))
			{
				return false;
			}
			HashSet<object> subs = _subscribers[typeof(TEvent)];
			return subs.Contains(handler);
		}

		public void Unsubscribe<TEvent>(MessageHandler<TEvent> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (_subscribers.ContainsKey(typeof(TEvent)))
			{
				HashSet<object> subs = _subscribers[typeof(TEvent)];
				subs.Remove(handler);
			}
		}

		public void Publish<TEvent>(TEvent data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (HasSubscribers<TEvent>())
			{
				foreach (MessageHandler<TEvent> h in _subscribers[typeof(TEvent)])
				{
					h.HandleMessage(data);
				}
			}
		}

		private bool HasSubscribers<TEvent>()
		{
			return _subscribers.ContainsKey(typeof(TEvent));
		}
	}
}