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

        public BlogPostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var blogPosts = await _mediator.Send(new GetAllRequest());
            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var blogPost = await _mediator.Send(new GetByIdRequest(id));
            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(blogPost);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            var blogPost = await _mediator.Send(request);
            return Ok(blogPost);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            var blogPost = await _mediator.Send(new UpdateByIdRequest(id, request.Title, request.Text, request.Tags));
            if (blogPost == null)
            {
                return BadRequest();
            }

            return Ok(blogPost);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var blogPost = await _mediator.Send(new DeleteByIdRequest(id));
            if (blogPost == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
