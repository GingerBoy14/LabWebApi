using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Comment;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Exceptions;
using LabWebAPI.Contracts.Services;
using LabWebAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LabWebApi.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        public CommentController(ICommentService commentService) { _commentService = commentService; }
        
           
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] SimpleCommentDTO model)
        {
            try
            {
                var commentData = new SimpleCommentWithUserIdDTO
                {
                    ProductId = model.ProductId,
                    PublicationDate = model.PublicationDate,
                    Text = model.Text,
                    UserWhoCreatedId = UserId
                };

                var result = await _commentService.AddComment(commentData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

    }
   
}
