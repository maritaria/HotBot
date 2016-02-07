using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotBot.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace HotBot.Core.Plugins.Tests
{
	[TestClass()]
	public class PluginDescriptionTests
	{
		[TestMethod()]
		public void Constructor_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => new PluginDescription(null, ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("!!!", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("A!0", ""));
			TestUtils.AssertArgumentException(() => new PluginDescription("ABC", null));
		}

		[TestMethod()]
		public void Constructor_ValidArguments()
		{
			var name = "HelloWorld1";
			var desc = "Description test!";
			var dependency = new Dependency();
			var pluginDescription = new PluginDescription(name, desc, dependency);

			Assert.AreEqual(name, pluginDescription.Name);
			Assert.AreEqual(desc, pluginDescription.Description);
			Assert.AreEqual(dependency, pluginDescription.Dependencies.First());
			Assert.AreEqual(1, pluginDescription.Dependencies.Count);
		}

		[TestMethod()]
		public void VerifyName_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName(null));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName(""));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("!"));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("a!a"));
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyName("a 0a"));
		}

		[TestMethod()]
		public void VerifyName_ValidArguments()
		{
			PluginDescription.VerifyName("a");
			PluginDescription.VerifyName("0");
			PluginDescription.VerifyName("a00aa0");
		}

		[TestMethod()]
		public void VerifyDescription_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => PluginDescription.VerifyDescription(null));
		}

		[TestMethod()]
		public void VerifyDescription_ValidArguments()
		{
			PluginDescription.VerifyDescription("");
			PluginDescription.VerifyDescription("a");
			PluginDescription.VerifyDescription("0");
			PluginDescription.VerifyDescription("a00aa0");
			PluginDescription.VerifyDescription("hello world!!");
			PluginDescription.VerifyDescription(":D");
		}
	}
}