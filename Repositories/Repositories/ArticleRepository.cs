using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
		private readonly WikyDbContext context;
		public ArticleRepository(WikyDbContext context)
		{
			this.context = context;
		}

		public async Task<List<Article>> GetAllArticlesAsync()
		{
			return await context.Articles.ToListAsync();
		}

		public async Task<Article> GetArticleByIdAsync(int id)
		{
			return await context.Articles.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Article> CreateArticleAsync(Article article)
		{
			await context.Articles.AddAsync(article);
			await context.SaveChangesAsync();
			return article;
		}

		public async Task<Article?> UpdateArticleAsync(Article article)
		{
			await context.Articles.Where(c => c.Id == article.Id).ExecuteUpdateAsync(
				updates => updates.SetProperty(c => c.Author, article.Author)
				.SetProperty(c => c.Content, article.Content)
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
