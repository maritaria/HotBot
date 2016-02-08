using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Tests
{
	[TestClass()]
	public class DictionaryMessageBusTests
	{
		[TestMethod()]
		public void DictionaryMessageBus_Constructor()
		{
			var bus = new DictionaryMessageBus();
		}

		[TestMethod()]
		public void Subscribe()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			TestUtils.AssertArgumentException(() => bus.Subscribe((MessageHandler<object>)null));
			bus.Subscribe(handler.Object);
		}

		[TestMethod()]
		public void Publish()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			var data = new object();
			bus.Subscribe(handler.Object);

			TestUtils.AssertArgumentException(() => bus.Publish<object>(null));

			bus.Publish(data);
			handler.Verify(h => h.HandleMessage(data), Times.Once());
		}

		[TestMethod()]
		public void Publish_OtherType()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<string>>();
			bus.Subscribe(handler.Object);
			bus.Publish(new object());
			handler.Verify(h => h.HandleMessage(It.Is<string>(o => true)), Times.Never());
			bus.Publish<object>("");
			handler.Verify(h => h.HandleMessage(It.Is<string>(o => true)), Times.Never());
		}

		[TestMethod()]
		public void Publish_NoSubscibers()
		{
			var bus = new DictionaryMessageBus();
			bus.Publish(new object());
		}

		[TestMethod()]
		public void Publish_MultiSub_RunOnce()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			var data = new object();
			bus.Subscribe(handler.Object);
			bus.Subscribe(handler.Object);
			bus.Publish(data);
			handler.Verify(h => h.HandleMessage(data), Times.Once());
		}

		[TestMethod()]
		public void IsSubscribed()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<string>>();

			TestUtils.AssertArgumentException(() => bus.IsSubscribed<object>(null));

			Assert.AreEqual(false, bus.IsSubscribed(handler.Object));
			bus.Subscribe(handler.Object);
			Assert.AreEqual(true, bus.IsSubscribed(handler.Object));
		}

		[TestMethod()]
		public void Unsubscribe()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			object data = new object();
			bus.Subscribe(handler.Object);
			bus.Unsubscribe(handler.Object);

			TestUtils.AssertArgumentException(() => bus.Unsubscribe((MessageHandler<object>)null));

			bus.Publish(data);
			handler.Verify(h => h.HandleMessage(data), Times.Never(), "Handler called after unsubscribe");
		}
	}
}