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
			var handler = new Mock<TestListener>();
			TestUtils.AssertArgumentException(() => bus.Subscribe(null));
			bus.Subscribe(handler);
		}

		[TestMethod()]
		public void Publish()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<TestListener>();
			string data = "asdf";
			bus.Subscribe(handler.Object);

			TestUtils.AssertArgumentException(() => bus.PublishSpecific<object>(null));

			bus.PublishSpecific(data);
			handler.Verify(h => h.Listen1(data), Times.Once());
		}

		[TestMethod()]
		public void Publish_OtherType()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<TestListener>();
			string value = "asdf";
			bus.Subscribe(handler.Object);
			bus.Publish(new object());
			handler.Verify(h => h.Listen1(value), Times.Never());
			bus.Publish(value);
			handler.Verify(h => h.Listen1(value), Times.Once());
		}

		[TestMethod()]
		public void Publish_NoSubscibers()
		{
			var bus = new DictionaryMessageBus();
			bus.PublishSpecific(new object());
		}

		[TestMethod()]
		public void Publish_MultiSub_RunOnce()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<TestListener>();
			string data = "asdf";
			bus.Subscribe(handler.Object);
			bus.Subscribe(handler.Object);
			bus.PublishSpecific(data);
			handler.Verify(h => h.Listen1(data), Times.Once());
		}

		[TestMethod()]
		public void IsSubscribed()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<TestListener>();

			TestUtils.AssertArgumentException(() => bus.IsSubscribed(null));

			Assert.AreEqual(false, bus.IsSubscribed(handler.Object));
			bus.Subscribe(handler.Object);
			Assert.AreEqual(true, bus.IsSubscribed(handler.Object));
		}

		[TestMethod()]
		public void Unsubscribe()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<TestListener>();
			string data = "asdf";
			bus.Subscribe(handler.Object);
			bus.Unsubscribe(handler.Object);

			TestUtils.AssertArgumentException(() => bus.Unsubscribe(null));

			bus.PublishSpecific(data);
			handler.Verify(h => h.Listen1(data), Times.Never(), "Handler called after unsubscribe");
		}

		private class TestListener
		{
			[Subscribe]
			public void Listen1(string value)
			{
			}

			[Subscribe]
			public void Listen2(DateTime value)
			{
			}
		}
	}
}