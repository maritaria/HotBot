using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBot.Core.Intercom
{
	public sealed class DictionaryMessageBus : MessageBus
	{
		private Dictionary<Type, Dictionary<object, MethodInfo>> _subscribers = new Dictionary<Type, Dictionary<object, MethodInfo>>();

		public DictionaryMessageBus()
		{
		}

		public void Publish(object data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Type publishType = GetPublishingType(data.GetType());
			PublishSpecific(publishType, data);
		}

		private Type GetPublishingType(Type dataType)
		{
			var attr = dataType.GetCustomAttribute<DefaultPublishTypeAttribute>();
			if (attr != null)
			{
				return attr.PublishType;
			}
			return dataType;
		}

		public void PublishSpecific(Type dataType, object instance)
		{
			if (_subscribers.ContainsKey(dataType))
			{
				var handlers = _subscribers[dataType];
				foreach (KeyValuePair<object, MethodInfo> handler in handlers)
				{
					handler.Value.Invoke(handler.Key, new object[] { instance });
				}
			}
		}

		public void Subscribe(object handler)
		{
			if (handler == null)
			{
				throw new ArgumentException("handler");
			}
			foreach (MethodInfo handlerMethod in handler.GetType().GetMethods())
			{
				Subscribe(handler, handlerMethod);
			}
		}

		private void Subscribe(object handler, MethodInfo handlerMethod)
		{
			foreach (SubscribeAttribute attr in handlerMethod.GetCustomAttributes<SubscribeAttribute>())
			{
				Subscribe(handler, handlerMethod, attr);
			}
		}

		private void Subscribe(object handler, MethodInfo handlerMethod, SubscribeAttribute attr)
		{
			Type publishedType = GetPublishingType(handlerMethod, attr);
			var subs = EnsureSubscribers(publishedType);
			if (!subs.ContainsKey(handler))
			{
				subs.Add(handler, handlerMethod);
			}
		}

		private Type GetPublishingType(MethodInfo handlerMethod, SubscribeAttribute attr)
		{
			if (attr.PublishedType != null)
			{
				return attr.PublishedType;
			}
			else
			{
				var paramInfo = handlerMethod.GetParameters();
				if (paramInfo.Length == 0)
				{
					throw new ArgumentException("Method cannot have zero parameters", "handlerMethod");
				}
				return paramInfo[0].ParameterType;
			}
		}

		private Dictionary<object, MethodInfo> EnsureSubscribers(Type publishedType)
		{
			if (!_subscribers.ContainsKey(publishedType))
			{
				_subscribers.Add(publishedType, new Dictionary<object, MethodInfo>());
			}
			return _subscribers[publishedType];
		}

		public void Unsubscribe(object handler)
		{
			foreach (Dictionary<object, MethodInfo> subs in _subscribers.Values)
			{
				subs.Remove(handler);
			}
		}

		public bool IsSubscribed(object handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			return _subscribers.Any(kv => kv.Value.ContainsKey(handler));
		}
	}
}