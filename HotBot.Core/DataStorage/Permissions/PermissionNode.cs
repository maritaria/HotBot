using System;
using System.Linq;

namespace HotBot.Core.DataStorage.Permissions
{
	public class PermissionNode
	{
		private string _path;

		public string Path
		{
			get { return _path; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("cannot be empty", "value");
				}
				_path = value;
			}
		}

		public PermissionType Type { get; set; }

		public PermissionNode(string path) : this(path, PermissionType.Grant)
		{
		}

		public PermissionNode(string path, PermissionType type)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("cannot be empty", "path");
			}
			_path = path;
			Type = type;
		}
	}
}