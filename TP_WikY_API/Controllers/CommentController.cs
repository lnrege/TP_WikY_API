using AutoMapper;
using DTOs.DTOsArticle;
using DTOs.DTOsComment;
using IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace TP_WikY_API.Controllers
{

	[Route("api/[controller]/[action]")]
	[ApiController]

	public class CommentController : ControllerBase
	{
		ICommentRepository commentRepository;
		IMapper mapper;
		UserManager<AppUser> userManager;
		public CommentController(ICommentRepository commentRepository, IMapper mapper, UserManager<AppUser> userManager)
		{
			this.commentRepository = commentRepository;
			this.userManager = userManager;
			this.mapper = mapper;
		}
		/// <summary>
		/// Get All the comments
		/// </summary>
		/// <returns>displays all the articles inlcuding the theme</returns>
		[HttpGet]
		public async Task<ActionResult> GetAllComments()
		{
			return Ok(await commentRepository.GetAllCommentsAsync());
		}

		/// <summary>
		/// Get a comment by Id
		/// </summary>
		/// <returns>display the comment following the id entered by the user</returns>
		[HttpGet]
		public async Task<ActionResult> GetCommentById(int id)
		{
			return Ok(await commentRepository.GetCommentByIdAsync(id));
		}

		/// <summary>
		/// Create a new comment
		/// </summary>
		/// <returns>the comment created</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> CreateComment(CommentAddDTO commentAddDTO)
		{
			try
			{
				var articleID = commentAddDTO.ArticleID;
				var comment = mapper.Map<Comment>(commentAddDTO);
				var user = await userManager.GetUserAsync(User);
				var result = await commentRepository.CreateCommentAsync(articleID, comment, user);

				return Ok(result);
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}

		/// <summary>
		/// Update an existing comment
		/// </summary>
		/// <returns>the updated comment</returns>
		[HttpPut]
		[Authorize]
		public async Task<ActionResult> UpdateComment(CommentUpdateDTO commentUpdateDTO)
		{
			try
			{
				var user = await userManager.GetUserAsync(User);
				var comment = mapper.Map<Comment>(commentUpdateDTO);
				if (comment.AppUserID == user.Id)
				{
					comment = await commentRepository.UpdateCommentAsync(comment);
					return Ok(await commentRepository.GetCommentByIdAsync(commentUpdateDTO.Id));
				}
				else
					return Problem("User is not autorized. Only the user who has update the Comment can delete it ");
			}

			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}
		/// <summary>
		/// Delete a comment by the entered Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> DeleteCommentByID(int id)
		{
			try
			{
				var user = await userManager.GetUserAsync(User);
				var article = await commentRepository.GetCommentByIdAsync(id);
				if (article.AppUserID == user.Id)
				{
					await commentRepository.DeleteCommentByIDAsync(id);
					return Ok("Comment deleted !");
				}
				else
					return Problem("User is not autorized. Only the user who has created the Comment can delete it ");
			}
			catch (Exception e)
			{
				return Problem(e!.InnerException!.Message);
			}
		}
	}
}
