using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class AppUser : IdentityUser
	{
		public Article Article { get; set; }
		public Comment Comment { get; set; }
		public int Age { get; set; }
	}
}
