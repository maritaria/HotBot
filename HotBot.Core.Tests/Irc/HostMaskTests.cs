using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HotBot.Core.Irc.Tests
{
	[TestClass()]
	public class HostMaskTests
	{
		public void Equals_SameValues()
		{
			string str = "test";
			HostMask mask1 = new HostMask() { Hostname = str, Nickname = str, Username = str };

			Assert.AreEqual(mask1.Hostname, str);
			Assert.AreEqual(mask1.Nickname, str);
			Assert.AreEqual(mask1.Username, str);

			HostMask mask2 = new HostMask() { Hostname = str, Nickname = str, Username = str };
			Assert.IsTrue(mask1.Equals(mask2));
			Assert.IsTrue(mask2.Equals(mask1));
		}

		public void Equals_DifferentValues()
		{
			HostMask mask = new HostMask() { Hostname = "test", Nickname = "test", Username = "test" };
			HostMask mask1 = new HostMask() { Hostname = "test1", Nickname = "test", Username = "test" };
			HostMask mask2 = new HostMask() { Hostname = "test", Nickname = "test2", Username = "test" };
			HostMask mask3 = new HostMask() { Hostname = "test", Nickname = "test", Username = "test3" };

			Assert.IsFalse(mask.Equals(mask1));
			Assert.IsFalse(mask.Equals(mask2));
			Assert.IsFalse(mask.Equals(mask3));
		}
	}
}