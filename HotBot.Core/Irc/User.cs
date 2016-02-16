using HotBot.Core.Permissions;
using System;

namespace HotBot.Core.Irc
{
	public interface User : Authorizer
	{
		string Name { get; }
	}
}