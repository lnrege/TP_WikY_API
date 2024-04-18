using DTOs.DTOsArticle;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
	public interface IArticleRepository
	{
		public Task<List<ArticleGetDTO>> GetAllArticlesAsync();

		public  Task<Article> GetArticleByIdAsync(int id);

		public Task<List<ArticleGetDTO>> CreateArticleAsync(Article article, AppUser user);

		public  Task<Article?> UpdateArticleAsync(Article article);

		public  Task<Article?> DeleteArticleByIDAsync(int id);
	}
}
