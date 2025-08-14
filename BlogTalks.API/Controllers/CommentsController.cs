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
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IMediator mediator, ILogger<CommentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Fetching all comments.");

            var comments = await _mediator.Send(new GetAllRequest());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            _logger.LogInformation("Fetching comment id {id}", id);

            var comment = await _mediator.Send(new GetByIdRequest(id));
            return Ok(comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            _logger.LogInformation("Creating a comment.");

            var comment = await _mediator.Send(request);
            return Ok(comment.Id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            _logger.LogInformation("Editing comment with id {id}.", id);

            var comment = await _mediator.Send(new UpdateByIdRequest(id, request.Text));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogInformation("Deleting comment with id {id}.", id);
            
            var comment = await _mediator.Send(new DeleteByIdRequest(id));
            return NoContent();
        }

        [HttpGet("/api/blogPosts/{id}/comments")]
        [Authorize]
        public async Task<ActionResult> GetByBlogPostId([FromRoute] int id)
        {
            _logger.LogInformation("Fetching comments for BlogPost with id {id}.", id);

            var comments = await _mediator.Send(new GetByBlogPostIdRequest(id));
            return Ok(comments);
        }
    }
}
