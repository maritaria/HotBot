using HotBot.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Core.Unity
{
	public sealed class RegisterNameAttribute : Attribute
	{
		public string Name { get; }
		public RegisterNameAttribute(string name)
		{
			Verify.NotNullOrEmpty(name, "name");
			Name = name;
		}
	}
}
