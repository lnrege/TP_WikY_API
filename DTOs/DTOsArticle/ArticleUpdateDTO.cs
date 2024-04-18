using Models;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOsArticle
{
    public class ArticleUpdateDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int ThemeID { get; set; }
	}
}
