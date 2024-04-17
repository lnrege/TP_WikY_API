using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comment
{
	public class CommentAddorUpdateDTO
	{
		public int Id { get; set; }
		public string Author { get; set; }

		[MaxLength(100)]
		public string Content { get; set; }

		public int ArticleID { get; set; }

	}
}
