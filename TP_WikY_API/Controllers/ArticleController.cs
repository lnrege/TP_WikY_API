using AutoMapper;
using DTOs.Article;
using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace TP_WikY_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]

	public class ArticleController : ControllerBase
	{
		IArticleRepository articleRepository;
		IMapper mapper;
		UserManager<AppUser> userManager;
		public ArticleController(IArticleRepository articleRepository, IMapper mapper, UserManager<AppUser> userManager)
		{
			this.articleRepository = articleRepository;
			this.userManager = userManager;
			this.mapper = mapper;
		}

	
		//SignInManager<AppUser> signInManager


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAllArticles()
		{
			return Ok(await articleRepository.GetAllArticlesAsync());
		}

		[HttpGet]

		public async Task<IActionResult> GetArticleById(int id)
		{
			return Ok(await articleRepository.GetArticleByIdAsync(id));
		}

		/// <summary>
		/// Create a new article
		/// </summary>
		/// <returns>article</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> CreateArticle(ArticleAddDTO articleAddDTO)
		{
			try
			{
				var article = mapper.Map<Article>(articleAddDTO);
				var user = await userManager.GetUserAsync(User);
				article = await articleRepository.CreateArticleAsync(article, user);

				return Ok(article);
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		/// <summary>
		/// Update an existing article
		/// </summary>
		/// <returns>the updated article</returns>
		[HttpPut]
		public async Task<ActionResult> UpdateArticle(ArticleUpdateDTO articleUpdateDTO)
		{
			try
			{
				var article = mapper.Map<Article>(articleUpdateDTO);

				article = await articleRepository.UpdateArticleAsync(article);

				return Ok(await articleRepository.GetArticleByIdAsync(articleUpdateDTO.Id));
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteArticleByID(int id)
		{
			return Ok(await articleRepository.DeleteArticleByIDAsync(id));
		}
	}
}
