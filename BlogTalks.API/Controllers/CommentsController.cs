using BlogTalks.Application.Comment.Commands;
using BlogTalks.Application.Comment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var comments = await _mediator.Send(new GetAllRequest());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var comment = await _mediator.Send(new GetByIdRequest(id));
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            var comment = await _mediator.Send(request);
            return Ok(comment.Id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            var comment = await _mediator.Send(new UpdateByIdRequest(id, request.Text));
            if (comment == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var comment = await _mediator.Send(new DeleteByIdRequest(id));
            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("/api/blogPosts/{id}/comments")]
        [Authorize]
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
