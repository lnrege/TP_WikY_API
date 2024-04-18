using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using DTOs.DTOsArticle;

namespace Repositories.Repositories
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly WikyDbContext context;
		public ArticleRepository(WikyDbContext context)
		{
			this.context = context;
		}

		public async Task<List<ArticleGetDTO>> GetAllArticlesAsync()
		{
			var articles = await context.Articles.Include(a => a.Theme)
			   .Include(a => a.AppUser)
			   .Select(a => new ArticleGetDTO
			   {
				   Title = a.Title,
				   Content = a.Content,
				   CreationDate = a.CreationDate,
				   LastModifiedDate = a.LastModifiedDate,
				   ThemeName = a.Theme.Label
			   })
			   .ToListAsync();
			return articles;
		}

		public async Task<Article> GetArticleByIdAsync(int id)
		{
			return await context.Articles.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<List<ArticleGetDTO>> CreateArticleAsync(Article article, AppUser user)
		{
			article.CreationDate = DateTime.Now;
			article.AppUserID = user.Id;
			await context.Articles.AddAsync(article);
			await context.SaveChangesAsync();

			var articles = await context.Articles.Where(a => a.Id == article.Id)
				.Include(a => a.Theme)
		   .Include(a => a.AppUser)
		   .Select(a => new ArticleGetDTO
		   {
			   Title = a.Title,
			   Content = a.Content,
			   CreationDate = a.CreationDate,
			   LastModifiedDate = a.LastModifiedDate,
			   ThemeName = a.Theme.Label
		   })
		   .ToListAsync();
			return articles;
		}

		public async Task<Article?> UpdateArticleAsync(Article article)
		{
			await context.Articles.Where(c => c.Id == article.Id).ExecuteUpdateAsync(
				updates => updates.SetProperty(c => c.Content, article.Content)
				.SetProperty(c => c.LastModifiedDate, DateTime.Now));
			return await context.Articles.FindAsync(article.Id);
		}

		public async Task<Article?> DeleteArticleByIDAsync(int id)
		{
			await context.Articles.Where(c => c.Id == id).ExecuteDeleteAsync();
			return await context.Articles.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
