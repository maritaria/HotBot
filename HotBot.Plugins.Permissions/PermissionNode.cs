using HotBot.Core.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotBot.Plugins.Permissions
{
	internal class PermissionNode
	{
		private const string UniqueConstraintName = "UniqueNodePerUser";

		[Key]
		public Guid NodeId { get; set; }

		[MaxLength(Verify.MaximumPermissionNodeLength)]
		[Index(UniqueConstraintName, 1, IsUnique = true)]
		public string Node { get; set; }

		[Index(UniqueConstraintName, 2, IsUnique = true)]
		public Guid UserId { get; set; }
	}
}