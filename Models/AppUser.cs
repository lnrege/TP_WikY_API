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
		public List<Article>? Articles { get; set; }
		public List<Comment>? Comments { get; set; }
		public DateTime Dob { get; set; }
		public string Name { get; set; }
	}
}
