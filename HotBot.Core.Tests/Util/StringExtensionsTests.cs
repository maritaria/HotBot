using HotBot.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Util.Tests
{
	[TestClass()]
	public class StringExtensionsTests
	{
		[TestMethod()]
		public void SplitOnce_Single()
		{
			TestUtils.AssertArgumentException(() => ((string)null).SplitOnce("asdf"));
			TestUtils.AssertArgumentException(() => ("asdf").SplitOnce((string)null));

			SplitOnce_SingleTest("Hello world :D", " ", "Hello", "world :D");
			SplitOnce_SingleTest("Hello world :D", " ", "Hello", "world :D");
			SplitOnce_SingleTest("Hello world :D", "Hello", "", " world :D");
			SplitOnce_SingleTest("HelloHello world :D", "Hello", "", "Hello world :D");
			SplitOnce_SingleTest("Hello world :D", "_", "Hello world :D", "");
			SplitOnce_SingleTest("Hello world :D", "", "Hello world :D", "");
		}

		private static void SplitOnce_SingleTest(string source, string splitter, string expectedResult, string expectedLeftover)
		{
			string[] result = source.SplitOnce(splitter);
			Assert.AreEqual(expectedResult, result[0]);
			Assert.AreEqual(expectedLeftover, result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Multi()
		{
			TestUtils.AssertArgumentException(() => ((string)null).SplitOnce("asdf", "asdf"));
			TestUtils.AssertArgumentException(() => ("asdf").SplitOnce((string)null, (string)null));

			SplitOnce_MultiTest("Hello world :D", " ", "Q", "Hello", "world :D");
			SplitOnce_MultiTest("Hello world :D", "Q", " ", "Hello", "world :D");
			SplitOnce_MultiTest("Hello world :D", " ", "Q", "Hello", "world :D");
			SplitOnce_MultiTest("Hello world :D", "Hello", "Q", "", " world :D");
			SplitOnce_MultiTest("Hello world :D", "world", "Hello", "", " world :D");
			SplitOnce_MultiTest("HelloHello world :D", "Hello", "Q", "", "Hello world :D");
			SplitOnce_MultiTest("Hello world :D", "_", "Q", "Hello world :D", "");
			SplitOnce_MultiTest("Hello world :D", "", "Q", "Hello world :D", "");

		}

		private static void SplitOnce_MultiTest(string source, string splitter1, string splitter2, string expectedResult, string expectedLeftover)
		{
			string[] result = source.SplitOnce(splitter1, splitter2);
			Assert.AreEqual(expectedResult, result[0]);
			Assert.AreEqual(expectedLeftover, result[1]);
		}
	}
}