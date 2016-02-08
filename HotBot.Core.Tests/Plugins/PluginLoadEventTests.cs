using HotBot.Testing;
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
		public void PluginLoadEvent_Constructor()
		{
			var plugin = new Mock<LoadablePlugin>();
			var pluginLoadEvent = new PluginLoadEvent(plugin.Object);

			TestUtils.AssertArgumentException(() => new PluginLoadEvent(null));

			Assert.AreEqual(plugin.Object, pluginLoadEvent.Plugin);
		}
	}
}