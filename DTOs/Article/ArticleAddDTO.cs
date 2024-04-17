using Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Article
{
    public class ArticleAddDTO
	{
		public int Id { get; set; }
		public string Author { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Priority Priority { get; set; }
		public int ThemeID { get; set; }

		//public string? AppUserID { get; set; }
		//public AppUser? AppUser { get; set; }

	}
}
