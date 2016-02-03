using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Util.Tests
{
	[TestClass()]
	public class StringExtensionsTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SplitOnce_Single_Source_Null()
		{
			string source = null;
			source.SplitOnce("asdf");
		}

		[TestMethod]
		public void SplitOnce_Single_Source_Empty()
		{
			string source = string.Empty;
			source.SplitOnce("asdf");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SplitOnce_Single_Splitter_Null()
		{
			string source = "asdf";
			string splitter = null;
			source.SplitOnce(splitter);
		}
		//TODO: Copy these tests for SplitOnce_Multi
		[TestMethod]
		public void SplitOnce_Single_Splitter_Empty()
		{
			string source = "asdf";
			source.SplitOnce(string.Empty);
		}

		[TestMethod()]
		public void SplitOnce_Single_ContainsSplitterMulitpleTimes()
		{
			string source = "Hello world :D";
			string[] result = source.SplitOnce(" ");
			Assert.AreEqual("Hello", result[0]);
			Assert.AreEqual("world :D", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Single_StartsWithSplitter()
		{
			string source = " Starts with splitter";
			string[] result = source.SplitOnce(" ");
			Assert.AreEqual("", result[0]);
			Assert.AreEqual("Starts with splitter", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Single_StartsWithDoubleSplitter()
		{
			string source = "  Starts with double splitter";
			string[] result = source.SplitOnce(" ", "w");
			Assert.AreEqual("", result[0]);
			Assert.AreEqual(" Starts with double splitter", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Single_NoSplit()
		{
			string source = "Hello world :D";
			string[] result = source.SplitOnce("_");
			Assert.AreEqual("Hello world :D", result[0]);
			Assert.AreEqual("", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Multi()
		{
			string source = "Hello world Q :D";
			string[] result = source.SplitOnce(" ", "Q");
			Assert.AreEqual("Hello", result[0]);
			Assert.AreEqual("world Q :D", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Multi_StartsWithSplitter()
		{
			string source = " Starts with splitter";
			string[] result = source.SplitOnce(" ", "w");
			Assert.AreEqual("", result[0]);
			Assert.AreEqual("Starts with splitter", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Multi_StartsWithDoubleSplitter()
		{
			string source = "  Starts with double splitter";
			string[] result = source.SplitOnce(" ", "w");
			Assert.AreEqual("", result[0]);
			Assert.AreEqual(" Starts with double splitter", result[1]);
		}

		[TestMethod()]
		public void SplitOnce_Multi_NoSplit()
		{
			string source = "Hello world :D";
			string[] result = source.SplitOnce("_", ",");
			Assert.AreEqual("Hello world :D", result[0]);
			Assert.AreEqual("", result[1]);
		}
		
	}
}