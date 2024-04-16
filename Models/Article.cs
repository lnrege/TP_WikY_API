using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class Article
	{
		public int Id { get; set; }

		[Required]
		public string Author { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Priority Priority { get; set; }

		public int ThemeID { get; set; }

		public string AppUserID { get; set; }
		public AppUser AppUser { get; set; }

		//navigation properties
		public Theme Theme { get; set; }
		public List<Comment?> Comments { get; set; }

	}
}
