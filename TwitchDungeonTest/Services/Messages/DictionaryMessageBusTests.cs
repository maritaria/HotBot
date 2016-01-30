using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TwitchDungeon.Services.Messages.Tests
{
	[TestClass()]
	public class DictionaryMessageBusTests
	{
		[TestMethod()]
		public void Constructor_Valid()
		{
			var bus = new DictionaryMessageBus();
		}

		[TestMethod()]
		public void Subscribe_Handler_Valid()
		{
			var bus = new DictionaryMessageBus();
			bus.Subscribe(new SimpleHandler<object>());
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Subscribe_Handler_NullArgument()
		{
			var bus = new DictionaryMessageBus();
			SimpleHandler<object> handler = null;
			bus.Subscribe(handler);
		}

		[TestMethod()]
		[ExpectedException(typeof(InvalidHandlerArgumentException))]
		public void Subscribe_Handler_AlreadyAssigned()
		{
			var bus = new DictionaryMessageBus();
			var handler = new SimpleHandler<object>();
			bus.Subscribe(handler);
			bus.Subscribe(handler);
		}

		[TestMethod()]
		public void Subscribe_Action_Valid()
		{
			var bus = new DictionaryMessageBus();
			bus.Subscribe(new MessageBusAction<object>(EmptyHandler));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Subscribe_Action_NullArgument()
		{
			var bus = new DictionaryMessageBus();
			MessageBusAction<object> nullHandler = null;
			bus.Subscribe(nullHandler);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Publish_Null()
		{
			var bus = new DictionaryMessageBus();
			bus.Publish<object>(null);
		}

		[TestMethod()]
		public void Publish_Valid()
		{
			var bus = new DictionaryMessageBus();
			bool executed = false;
			bus.Subscribe<object>((passedBus, passedData) =>
			{
				executed = true;
			});
			bus.Publish(new object());
			if (!executed)
			{
				Assert.Fail("Handler not executed");
			}
		}

		[TestMethod()]
		public void Publish_Valid_PassedValues_Callback()
		{
			var bus = new DictionaryMessageBus();
			object data = new object();
			bus.Subscribe<object>((passedBus, passedData) =>
			{
				Assert.AreEqual(bus, passedBus);
				Assert.AreEqual(data, passedData);
			});
			bus.Publish(data);
		}

		[TestMethod()]
		public void Publish_Valid_PassedValues_Handler()
		{
			var bus = new DictionaryMessageBus();
			object data = new object();
			SimpleHandler<object> handler = new SimpleHandler<object>();
			handler.Callback = (passedBus, passedData) =>
			{
				Assert.AreEqual(bus, passedBus);
				Assert.AreEqual(data, passedData);
			};
			bus.Subscribe<object>(handler);
			bus.Publish(data);
		}

		[TestMethod()]
		public void Publish_Valid_RunOnce()
		{
			var bus = new DictionaryMessageBus();
			int invokeCount = 0;
			SimpleHandler<object> handler = new SimpleHandler<object>();
			handler.Callback = (passedBus, passedData) =>
			{
				invokeCount++;
			};
			bus.Subscribe<object>(handler);
			bus.Publish(new object());
			Assert.AreEqual(1, invokeCount);
		}

		[TestMethod()]
		public void Publish_OtherType()
		{
			var bus = new DictionaryMessageBus();
			bus.Subscribe<string>((passedBus, passedData) =>
			{
				Assert.Fail("Handler executed for wrong type");
			});
			bus.Publish(new object());
		}

		[TestMethod()]
		public void Publish_NoSubscribers()
		{
			var bus = new DictionaryMessageBus();
			Assert.AreEqual(false, bus.HasSubscribers<object>());
			bus.Publish(new object());
		}

		private void EmptyHandler<TData>(MessageBus bus, TData data)
		{
		}

		private class SimpleHandler<T> : MessageHandler<T>
		{
			public Action<MessageBus, T> Callback;

			protected override void Handle(T item)
			{
				Callback(Bus, item);
			}
		}
	}
}