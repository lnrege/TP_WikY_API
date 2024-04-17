using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Comment
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModifiedDate { get; set; }

		[MaxLength(100)]
		public string Content { get; set; }

		public string? AppUserID { get; set; }
		public AppUser? AppUser { get; set; }
		public int ArticleID { get; set; }

		//navigation properties
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public Article Article { get; set; }
	}
}
