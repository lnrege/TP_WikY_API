using DTOs.DTOsAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics.CodeAnalysis;

namespace TP_WikY_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class AuthController : ControllerBase
	{
		UserManager<AppUser> userManager;
		//SignInManager<AppUser> signInManager;
		//RoleManager<IdentityRole> roleManager;
		public AuthController(
			UserManager<AppUser> userManager)
		//SignInManager<AppUser> signInManager)
		//RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			//this.signInManager = signInManager;
			//this.roleManager = roleManager;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> CreateUser(NewUserDTO userDTO)
		{
			var appUser = new AppUser { UserName = userDTO.UserName, Name = userDTO.Name, Email = userDTO.Email, Dob = userDTO.Dob };
			var age = DateTime.Today.Year - appUser.Dob.Year;

			if (age >= 18)
			{
				var result = await userManager.CreateAsync(appUser, userDTO.Password);
				if (result.Succeeded)
				{
					return Ok("User created !");
				}
				else
					return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
			}
			else
			{
				return Problem("User's age is under 18 years old");
			}
		}


		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO userDTO)
		{
			var appUser = await userManager.GetUserAsync(User);

			var result = await userManager.ChangePasswordAsync(appUser, userDTO.CurrentPassword, userDTO.NewPassword);

			if (result.Succeeded)
			{
				return Ok();
			}
			else
				return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
		}


		//[AllowAnonymous]
		//[HttpPost]
		//public async Task<IActionResult> CreateRoleAdmin()
		//{
		//	if (!await roleManager.RoleExistsAsync("ADMIN"))
		//	{
		//		var result = await roleManager.CreateAsync(new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" });

		//		if (result.Succeeded)
		//		{
		//			return Ok();
		//		}
		//		else
		//			return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));

		//	}

		//	return Ok();
		//}
		//[HttpGet]
		//public async Task<IActionResult> AddUserToRoleAdmin()
		//{
		//	var appUser = await userManager.GetUserAsync(User);

		//	await userManager.AddToRoleAsync(appUser, "ADMIN");

		//	return Ok();
		//}
	}
}

