using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOsArticle
{
	public class ArticleGetDTO
	{
		public DateTime CreationDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string ThemeName { get; set; }

	}
}
