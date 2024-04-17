using AutoMapper;
using DTOs.Comment;
using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Repositories;

namespace TP_WikY_API.Controllers
{
	
	[Route("api/[controller]/[action]")]
	[ApiController]
	//[Authorize]

	public class CommentController(ICommentRepository commentRepository, IMapper mapper) : ControllerBase
	{
				
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
		public async Task<IActionResult> CreateComment(CommentAddorUpdateDTO commentAddDTO)
		{
			//await commentRepository.CreateCommentAsync(comment);
			//return Ok(comment);

			try
			{
				var comment = mapper.Map<Comment>(commentAddDTO);

				comment = await commentRepository.CreateCommentAsync(comment);

				return Ok(comment);
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateComment(CommentAddorUpdateDTO commentUpdateDTO)
		{
			try
			{
				var comment = mapper.Map<Comment>(commentUpdateDTO);

				comment = await commentRepository.UpdateCommentAsync(comment);

				return Ok(await commentRepository.GetCommentByIdAsync(commentUpdateDTO.Id));
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCommentByID(int id)
		{
			return Ok(await commentRepository.DeleteCommentByIDAsync(id));
		}
	}
}
