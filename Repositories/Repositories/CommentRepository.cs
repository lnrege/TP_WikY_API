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

		public async Task<List<Comment>> GetAllCommentsAsync()
		{
			return await context.Comments.ToListAsync();
		}

		public async Task<Comment> GetCommentByIdAsync(int id)
		{
			return await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Comment> CreateCommentAsync(Comment comment)
		{
			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();
			return comment;
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
