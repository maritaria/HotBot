using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace HotBot.Core.Plugins.Tests
{
	[TestClass()]
	public class PluginLoadEventTests
	{
		[TestMethod()]
		public void Constructor_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => new PluginLoadEvent(null));
		}

		[TestMethod()]
		public void Constructor_ValidArguments()
		{
			var plugin = new Mock<Plugin>();
			var pluginLoadEvent = new PluginLoadEvent(plugin.Object);
			Assert.AreEqual(plugin.Object, pluginLoadEvent.Plugin);
		}
	}
}