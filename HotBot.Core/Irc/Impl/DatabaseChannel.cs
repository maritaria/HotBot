using HotBot.Core.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotBot.Core.Irc.Impl
{
	internal class DatabaseChannel
	{
		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(25)]
		public string Name { get; private set; }

		public DatabaseChannel(string name) : this()
		{
			Verify.ChannelName(name, "name");
			Name = name;
		}

		private DatabaseChannel()
		{
			Id = Guid.NewGuid();
		}

		public override string ToString()
		{
			return "#" + Name;
		}
	}
}