using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.DataStorage
{
	public class Channel
	{
		[Key]
		public string Name { get; }
	}
}
