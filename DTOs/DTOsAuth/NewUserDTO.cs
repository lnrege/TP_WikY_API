using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DTOs.DTOsAuth
{
	public class NewUserDTO
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public DateTime Dob { get; set; }
	}
}
