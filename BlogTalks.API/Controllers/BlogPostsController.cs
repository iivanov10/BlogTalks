using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogTalks.Application.BlogPost.Queries;
using BlogTalks.Application.BlogPost.Commands;
using Microsoft.AspNetCore.Authorization;

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BlogPostsController> _logger;

        public BlogPostsController(IMediator mediator, ILogger<BlogPostsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get([FromQuery]int? pageNumber, int? pageSize, string? searchWord, string? tag)
        {
            _logger.LogInformation("Fetching all BlogPosts by searchWord {searchWord} with tag {tag}.", searchWord, tag);

            var blogPosts = await _mediator.Send(new GetAllRequest(pageNumber ?? 1, pageSize ?? 10, searchWord, tag));
            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            _logger.LogInformation("Fetching BlogPost with id {id}.", id);

            var blogPost = await _mediator.Send(new GetByIdRequest(id));
            return Ok(blogPost);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            _logger.LogInformation("Creating a BlogPost.");

            var blogPost = await _mediator.Send(request);
            return Ok(blogPost);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            _logger.LogInformation("Editing BlogPost with id {id}.", id);

            var blogPost = await _mediator.Send(new UpdateByIdRequest(id, request.Title, request.Text, request.Tags));
            return Ok(blogPost);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogInformation("Deleting BlogPost with id {id}.", id);

            var blogPost = await _mediator.Send(new DeleteByIdRequest(id));
            return NoContent();
        }
    }
}
