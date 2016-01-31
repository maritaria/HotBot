using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Services
{
	public sealed class DictionaryMessageBus : MessageBus
	{
		private Dictionary<Type, HashSet<object>> _subscribers = new Dictionary<Type, HashSet<object>>();

		public DictionaryMessageBus()
		{
		}

		public void Subscribe<TData>(MessageHandler<TData> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!_subscribers.ContainsKey(typeof(TData)))
			{
				_subscribers.Add(typeof(TData), new HashSet<object>());
			}
			HashSet<object> subs = _subscribers[typeof(TData)];
			subs.Add(handler);
		}

		public bool IsSubscribed<TData>(MessageHandler<TData> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!_subscribers.ContainsKey(typeof(TData)))
			{
				return false;
			}
			HashSet<object> subs = _subscribers[typeof(TData)];
			return subs.Contains(handler);
		}

		public void Unsubscribe<TData>(MessageHandler<TData> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (_subscribers.ContainsKey(typeof(TData)))
			{
				HashSet<object> subs = _subscribers[typeof(TData)];
				subs.Remove(handler);
			}
		}

		public void Publish<TData>(TData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (HasSubscribers<TData>())
			{
				foreach (MessageHandler<TData> h in _subscribers[typeof(TData)])
				{
					h.HandleMessage(data);
				}
			}
		}

		private bool HasSubscribers<TData>()
		{
			return _subscribers.ContainsKey(typeof(TData));
		}
	}
}