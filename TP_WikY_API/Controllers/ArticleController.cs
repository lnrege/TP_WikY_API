using AutoMapper;
using DTOs.Article;
using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TP_WikY_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	//[Authorize]
	public class ArticleController(IArticleRepository articleRepository, IMapper mapper) : ControllerBase
	{
		[HttpGet]

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
		public async Task<ActionResult> CreateArticle(ArticleAddDTO articleAddDTO)
		{
			try
			{
				var article = mapper.Map<Article>(articleAddDTO);

				article = await articleRepository.CreateArticleAsync(article);

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
		public async Task<ActionResult> UpdateArticle(ArticleAddDTO articleAddDTO)
		{
			try
			{
				var article = mapper.Map<Article>(articleAddDTO);

				article = await articleRepository.UpdateArticleAsync(article);

				return Ok(await articleRepository.GetArticleByIdAsync(articleAddDTO.Id));
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
