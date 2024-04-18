using AutoMapper;
using DTOs.DTOsArticle;
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


		/// <summary>
		/// Get All the articles
		/// </summary>
		/// <returns>displays all the articles inlcuding the theme</returns>
		[HttpGet]
		public async Task<ActionResult> GetAllArticles()
		{
			return Ok(await articleRepository.GetAllArticlesAsync());
		}

		/// <summary>
		/// Get the article by ID
		/// </summary>
		/// <returns>displays the article which latches the id entered</returns>
		[HttpGet]
		public async Task<ActionResult> GetArticleById(int id)
		{
			return Ok(await articleRepository.GetArticleByIdAsync(id));
		}

		/// <summary>
		/// Create a new article
		/// </summary>
		/// <returns>the article created</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> CreateArticle(ArticleAddDTO articleAddDTO)
		{
			try
			{
				var article = mapper.Map<Article>(articleAddDTO);
				var user = await userManager.GetUserAsync(User);
				var result = await articleRepository.CreateArticleAsync(article, user);

				return Ok(result);
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
		[Authorize]
		public async Task<ActionResult> UpdateArticle(ArticleUpdateDTO articleUpdateDTO)
		{
			try
			{
				var user = await userManager.GetUserAsync(User);
				var article = mapper.Map<Article>(articleUpdateDTO);
				if (article.AppUserID == user.Id)
				{
					article = await articleRepository.UpdateArticleAsync(article);
					return Ok(await articleRepository.GetArticleByIdAsync(articleUpdateDTO.Id));
				}
				else return Problem("User is not autorized. Only the user who has update the Article can delete it ");
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		/// <summary>
		/// Delete an article by the entered Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> DeleteArticleByID(int id)
		{
			try
			{
				var user = await userManager.GetUserAsync(User);
				var article = await articleRepository.GetArticleByIdAsync(id);
				if (article.AppUserID == user.Id)
				{
					await articleRepository.DeleteArticleByIDAsync(id);
					return Ok("Article deleted !");
				}
				else
					return Problem("User is not autorized. Only the user who has created the Article can delete it ");
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}
	}
}
