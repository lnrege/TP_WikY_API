using DTOs.DTOsArticle;
using DTOs.DTOsComment;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly WikyDbContext context;
		public CommentRepository(WikyDbContext context)
		{
			this.context = context;
		}

		public async Task<List<CommentGetDTO>> GetAllCommentsAsync()
		{
			var comments = await context.Comments
				.Include(c => c.AppUser)
				.Include(c => c.Article).ThenInclude(a => a.Theme)
				.Select(c => new CommentGetDTO
				{
					CreationDate = c.CreationDate,
					LastModifiedDate = c.LastModifiedDate,
					Content = c.Content,
					UserName = c.AppUser.UserName,
					ArticleID = c.ArticleID,
					ArticleTitle = c.Article.Title,
					ThemeName = c.Article.Theme.Label
				}).ToListAsync();
			return comments;
		}

		public async Task<Comment> GetCommentByIdAsync(int id)
		{
			return await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<List<CommentCreateDTO>> CreateCommentAsync(int articleID, Comment comment, AppUser user)
		{
			comment.CreationDate = DateTime.Now;
			comment.AppUserID = user.Id;
			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();

			var result = await context.Comments.Where(c => c.Id == comment.Id)
				.Include(c => c.AppUser)
				.Include(c => c.Article).ThenInclude(a => a.Theme)
				.Select(c => new CommentCreateDTO
				{
					Content = c.Content,
					UserName = c.AppUser.UserName,
					ArticleID = articleID,
					ArticleTitle = c.Article.Title,
					ThemeName = c.Article.Theme.Label
				})
			.ToListAsync();
			return result;
		}
		public async Task<Comment?> UpdateCommentAsync(Comment comment)
		{
			await context.Comments.Where(c => c.Id == comment.Id).ExecuteUpdateAsync(
				updates => updates.SetProperty(c => c.Content, comment.Content)
				.SetProperty(c => c.LastModifiedDate, DateTime.Now));
			return await context.Comments.FindAsync(comment.Id);
		}

		public async Task<Comment?> DeleteCommentByIDAsync(int id)
		{
			await context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
			return await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
		}

	}
}
