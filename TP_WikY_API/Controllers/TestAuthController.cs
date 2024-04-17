using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TP_WikY_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TestAuthController(IArticleRepository articleRepository) : ControllerBase
	{
		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			return Ok($"Auth !{User.Identity.Name} {User.FindFirst(ClaimTypes.NameIdentifier).Value}");
		}

		//[HttpPost]
		//public IActionResult CreateArticle(string title, string content)
		//{
		//	var article=articleRepository.Articles
		//}
	}
}
