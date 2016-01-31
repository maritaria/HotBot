using System;
using System.Linq;

namespace HotBot.Core.Util
{
	public static class StringExtensions
	{
		public static string[] SplitOnce(this string source, string splitters)
		{
			string[] parts = source.Split(new string[] { splitters }, StringSplitOptions.None);
			string remainder = string.Join(splitters, parts.Skip(1));
			return new string[] { parts[0], remainder };
		}

		public static string[] SplitOnce(this string source, params string[] splitters)
		{
			string[] parts = source.Split(splitters, StringSplitOptions.None);
			string remainder = source.Substring(parts[0].Length);
			foreach (string splitter in splitters)
			{
				if (remainder.StartsWith(splitter))
				{
					remainder = remainder.Substring(splitter.Length);
					break;
				}
			}
			return new string[] { parts[0], remainder };
		}
	}
}