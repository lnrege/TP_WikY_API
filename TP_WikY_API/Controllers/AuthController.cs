using DTOs.DTOsAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TP_WikY_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class AuthController : ControllerBase
	{
		UserManager<AppUser> userManager;
		//SignInManager<AppUser> signInManager;
		RoleManager<IdentityRole> roleManager;
		public AuthController(
			UserManager<AppUser> userManager,
			//SignInManager<AppUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			//this.signInManager = signInManager;
			this.roleManager = roleManager;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> CreateUser(NewUserDTO userDTO)
		{
			var appUser = new AppUser { Name=userDTO.Name, UserName = userDTO.UserName, Dob=userDTO.Dob }; // Add other properties if needed
			var result = await userManager.CreateAsync(appUser, userDTO.Password);

			if (result.Succeeded)
			{
				// use appUser to create other item with you context if needed

				return Ok("User created !");
			}
			else
				return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));
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


		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateRoleAdmin()
		{
			if (!await roleManager.RoleExistsAsync("ADMIN"))
			{
				var result = await roleManager.CreateAsync(new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" });

				if (result.Succeeded)
				{
					return Ok();
				}
				else
					return Problem(string.Join(" | ", result.Errors.Select(e => e.Description)));

			}

			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> AddUserToRoleAdmin()
		{
			var appUser = await userManager.GetUserAsync(User);

			await userManager.AddToRoleAsync(appUser, "ADMIN");

			return Ok();
		}
	}
}
