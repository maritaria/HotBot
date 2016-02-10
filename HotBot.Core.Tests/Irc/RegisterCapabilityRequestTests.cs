using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotBot.Core.Irc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotBot.Testing;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class RegisterCapabilityRequestTests
	{
		[TestMethod()]
		public void RegisterCapabilityRequest_Constructor()
		{
			var capability = "testCapability";
			var request = new RegisterCapabilityRequest(capability);

			Assert.AreEqual(capability, request.Capability);

			TestUtils.AssertArgumentException(() => new RegisterCapabilityRequest(null));
			TestUtils.AssertArgumentException(() => new RegisterCapabilityRequest(""));
		}

		[TestMethod()]
		public void VerifyCapability()
		{
			TestUtils.AssertArgumentException(() => RegisterCapabilityRequest.VerifyCapability(null));
			TestUtils.AssertArgumentException(() => RegisterCapabilityRequest.VerifyCapability(""));
		}
	}
}