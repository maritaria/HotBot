using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBot.Plugins.Permissions
{
	internal sealed class UserGroup
	{
		[Key]
		public Guid UserId { get; set; }
		
		public string Group { get; set; }
	}
}
