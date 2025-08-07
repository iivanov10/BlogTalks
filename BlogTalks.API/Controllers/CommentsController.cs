using BlogTalks.Application.Comment.Commands;
using BlogTalks.Application.Comment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var comments = await _mediator.Send(new GetAllRequest());
            return Ok(comments);
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var comment = await _mediator.Send(new GetByIdRequest(id));
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            var comment = await _mediator.Send(request);
            return Ok(comment.Id);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            var comment = await _mediator.Send(new UpdateByIdRequest(id, request.Text));
            if (comment == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var comment = await _mediator.Send(new DeleteByIdRequest(id));
            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET api/blogPosts/<CommentsController>/5/comments
        [HttpGet("/api/blogPosts/{id}/comments")]
        public async Task<ActionResult> GetByBlogPostId([FromRoute] int id)
        {
            var comments = await _mediator.Send(new GetByBlogPostIdRequest(id));
            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }
    }
}
