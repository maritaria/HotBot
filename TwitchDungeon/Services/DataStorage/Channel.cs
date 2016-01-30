using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.DataStorage
{
	public class Channel
	{
		public static readonly string ChannelPrefix = "#";

		[Key]
		public Guid Id { get; private set; }

		[Index(IsUnique = true)]
		[StringLength(25)]
		public string Name { get; private set; }

		public Channel(string name) : this()
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Name = name;
		}

		protected Channel()
		{
			Id = Guid.NewGuid();
		}

		public override string ToString()
		{
			return ChannelPrefix + Name;
		}

	}
}
