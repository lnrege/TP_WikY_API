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
	public class CommentAddDTO
	{
		public string Content { get; set; }
		public int ArticleID { get; set; }

	}
}
