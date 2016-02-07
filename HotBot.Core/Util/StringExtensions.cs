using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBot.Core.Util
{
	public static class StringExtensions
	{
		/// <summary>
		/// Splits a string at the first occurence of a given splitter string and returns both parts.
		/// Neither parts contains the splitter.
		/// </summary>
		/// <param name="source">The string to be split.</param>
		/// <param name="splitter">The string that splits the source in two parts.</param>
		/// <returns>
		/// An array containing two strings.
		/// The first is the string prior to the first occurrence of the splitter.
		/// The second is the string after the first occurrence of the splitter.
		/// </returns>
		public static string[] SplitOnce(this string source, string splitter)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (splitter == null)
			{
				throw new ArgumentNullException("splitter");
			}
			string[] parts = source.Split(new string[] { splitter }, StringSplitOptions.None);
			string remainder = string.Join(splitter, parts.Skip(1));
			return new string[] { parts[0], remainder };
		}

		/// <summary>
		/// Splits a given string at the first occurrence of any of the given splitters.
		/// </summary>
		/// <param name="source">The string to be split.</param>
		/// <param name="splitters">A set of strings to split the string by. Only one is used.</param>
		/// <returns>
		/// An array containing two strings.
		/// The first is the string prior to the first occurrence of the splitter.
		/// The second is the string after the first occurrence of the splitter.
		/// </returns>
		public static string[] SplitOnce(this string source, params string[] splitters)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
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

		/// <summary>
		/// Splits a string multiple times in order by a set of splitters.
		/// The string is split in order of the given splitters and each splitter is used only once.
		/// </summary>
		/// <param name="source">The string to split</param>
		/// <param name="orderedSplitters">A set of splitters which will be used one-by-one to split the string.</param>
		/// <returns>
		/// Yield returns the parts the source string is split into.
		/// Including the remainder after all splitters have been used.
		/// </returns>
		public static IEnumerable<string> SplitMultiple(this string source, params string[] orderedSplitters)
		{
			string remaining = source;
			foreach (string splitter in orderedSplitters)
			{
				string[] parts = remaining.SplitOnce(splitter);
				remaining = parts[0];
				yield return parts[1];
			}
			yield return remaining;
		}
	}
}