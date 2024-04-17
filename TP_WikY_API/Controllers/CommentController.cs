using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TP_WikY_API.Controllers
{
	
	[Route("api/[controller]/[action]")]
	[ApiController]
	//[Authorize]

	public class CommentController : ControllerBase
	{
		
		ICommentRepository commentRepository;
		public CommentController(ICommentRepository commentRepository)
		{
			this.commentRepository = commentRepository;
		}
		
		[HttpGet]
		
		public async Task<IActionResult> GetAllComments()
		{
			return Ok(await commentRepository.GetAllCommentsAsync());
		}

		[HttpGet]
		
		public async Task<IActionResult> GetCommentById(int id)
		{
			return Ok(await commentRepository.GetCommentByIdAsync(id));
		}


		[HttpPost]
		public async Task<IActionResult> CreateComment(Comment comment)
		{
			await commentRepository.CreateCommentAsync(comment);
			return Ok(comment);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateComment(Comment comment)
		{
			await commentRepository.UpdateCommentAsync(comment);
			return Ok(await commentRepository.GetCommentByIdAsync(comment.Id));
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCommentByID(int id)
		{
			return Ok(await commentRepository.DeleteCommentByIDAsync(id));
		}
	}
}
