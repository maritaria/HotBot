using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotBot.Core.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Irc.Tests
{
	//Todo; collapse all constructor tests into 1 test; requires custom assert util class
	[TestClass()]
	public class WhisperUserEventTests
	{
		[TestMethod()]
		public void Constructor_InvalidArguments()
		{
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(null, "test", "test"));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(new Channel("test"), null, "test"));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(new Channel("test"), "", "test"));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(new Channel("test"), "t", "test"));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(new Channel("test"), "123456789012345678901234567890", "test"));
			TestUtils.AssertArgumentException(() => new WhisperUserRequest(new Channel("test"), "test", null));
		}
		
		[TestMethod()]
		public void Constructor_Valid()
		{
			new WhisperUserRequest(new Channel("test"), "test", "test");
		}
	}
}