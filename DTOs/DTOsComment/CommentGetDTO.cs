using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOsComment
{
	public class CommentGetDTO
	{
		public DateTime CreationDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public string Content { get; set; }
		public string UserName { get; set; }
		public int ArticleID { get; set; }
		public string ArticleTitle { get; set; }
		public string ThemeName { get; set; }

	}
}
