using System;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace efcore2_webapi.Webapp.Controllers
{
    [Route("/api/[controller]")]
    public class CommentController : Controller
    {

        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;            
        }

        [HttpPost()]
        public IActionResult PostComment([FromBody] Comment comment)
        {
            try
            {
                if (comment.CommentedOn == DateTime.MinValue)
                    comment.CommentedOn = DateTime.UtcNow;
                    
                _commentRepository.Save(comment);

                return Ok();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }            
        }
    }
}