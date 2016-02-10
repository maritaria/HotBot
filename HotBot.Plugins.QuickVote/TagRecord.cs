using System;
using System.Linq;

namespace HotBot.Plugins.QuickVote
{
	//TODO: support dynamic tag transformations (keep track of alternatives and automatically switch the main tag)
	public sealed class TagRecord
	{
		/// <summary>
		/// Gets the tag the <see cref="TagRecord"/> represents.
		/// </summary>
		public string Tag { get; }

		/// <summary>
		/// Gets or sets the timestamp at which the tag will expire.
		/// </summary>
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// Gets or sets the number of times the tag has been mentioned.
		/// </summary>
		public int MentionCount { get; set; }//TODO: deny negative values

		/// <summary>
		/// Gets whether the <see cref="TagRecord"/> has been expired.
		/// </summary>
		public bool IsExpired => (DateTime.Now > ExpirationDate) && !IsPermanent;

		/// <summary>
		/// Gets or sets whether the <see cref="TagRecord"/> can expire.
		/// When set to false IsExpired will always return false.
		/// </summary>
		public bool IsPermanent { get; set; } = false;

		public TagRecord(string tag)
		{
			try
			{
				VerifyTag(tag);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "tag", ex);
			}
			Tag = tag;
		}

		public static void VerifyTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (tag == string.Empty)
			{
				throw new ArgumentException("Cannot be an empty string", "tag");
			}
		}
	}
}