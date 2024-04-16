﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Theme
	{
		public int Id { get; set; }

		[Required]
		public string Label { get; set; }

		//navigation properties
		public List<Article> Articles { get; set; }
	}
}
