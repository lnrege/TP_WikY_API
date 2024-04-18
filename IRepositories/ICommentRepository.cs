using DTOs.DTOsComment;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
	public interface ICommentRepository
	{
		public Task<List<CommentGetDTO>> GetAllCommentsAsync();
		public Task<Comment> GetCommentByIdAsync(int id);
		public Task<List<CommentCreateDTO>> CreateCommentAsync(int articleID, Comment comment, AppUser user);
		public Task<Comment?> UpdateCommentAsync(Comment comment);
		public Task<Comment?> DeleteCommentByIDAsync(int id);
	}
}
