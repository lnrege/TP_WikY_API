﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOsComment
{
	public class CommentUpdateDTO
	{
		public int Id { get; set; }

		[MaxLength(100)]
		public string Content { get; set; }
	}
}
