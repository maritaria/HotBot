using TwitchDungeon.Services.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Moq;

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
		[ExpectedException(typeof(ArgumentNullException))]
		public void Subscribe_Null()
		{
			var bus = new DictionaryMessageBus();
			MessageHandler<object> handler = null;
			bus.Subscribe(handler);
		}

		[TestMethod()]
		public void Subscribe_Valid()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			bus.Subscribe(handler.Object);
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
			var handler = new Mock<MessageHandler<object>>();
			var data = new object();
			bus.Subscribe(handler.Object);
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
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsSubscribed_Null()
		{
			var bus = new DictionaryMessageBus();
			bus.IsSubscribed<object>(null);
		}

		[TestMethod()]
		public void IsSubscribed_Valid()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<string>>();
			Assert.AreEqual(false, bus.IsSubscribed(handler.Object));
			bus.Subscribe<string>(handler.Object);
			Assert.AreEqual(true, bus.IsSubscribed(handler.Object));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Unsubscribe_Null()
		{
			var bus = new DictionaryMessageBus();
			MessageHandler<object> handler = null;
			bus.Unsubscribe(handler);
		}

		[TestMethod()]
		public void Unsubscribe_Valid()
		{
			var bus = new DictionaryMessageBus();
			var handler = new Mock<MessageHandler<object>>();
			object data = new object();
			bus.Subscribe(handler.Object);
			bus.Unsubscribe(handler.Object);
			bus.Publish(data);
			handler.Verify(h => h.HandleMessage(data), Times.Never(), "Handler called after unsubscribe");
		}
	}
}