using System;
using System.Linq;

namespace HotBot.Core.Intercom
{
	public interface MessageBus
	{
		/// <summary>
		/// Subscribes a handler to start receiving messages from the current <see cref="MessageBus"/>.
		/// All methods decorated with a <see cref="SubscribeAttribute"/> will be registered.
		/// </summary>
		void Subscribe(object handler);
		/// <summary>
		/// Unsubscribes a handler so it no longer receives messages from the current <see cref="MessageBus"/>.
		/// </summary>
		/// <param name="handler">The object of which to unsubscribe the methods</param>
		void Unsubscribe(object handler);
		/// <summary>
		/// Checks whether a handler has any subscriptions on the current <see cref="MessageBus"/>.
		/// </summary>
		/// <param name="handler">The object to subscribe the methods of</param>
		bool IsSubscribed(object handler);

		/// <summary>
		/// Publishes an object on it'It s default publishing type.
		/// By default this is the type of the instance itself.
		/// You can modify this behaviour by decorating the declaring type with a <see cref="DefaultPublishTypeAttribute"/>.
		/// </summary>
		/// <param name="data">The object to be published</param>
		void Publish(object data);

		/// <summary>
		/// Publishes an object of a specific type to all registered handlers for that type.
		/// </summary>
		/// <param name="dataType">The type for which to publish the data</param>
		/// <param name="instance">An instance of the dataType</param>
		/// <exception cref="ArgumentException">The given instance is not of the given dataType</exception>
		void PublishSpecific(Type dataType, object instance);


	}
}