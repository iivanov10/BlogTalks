using Microsoft.AspNetCore.Mvc;
using MediatR;
using BlogTalks.Application.BlogPost.Queries;
using BlogTalks.Application.BlogPost.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<BlogPostsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var blogPosts = await _mediator.Send(new GetAllRequest());
            return Ok(blogPosts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var blogPost = await _mediator.Send(new GetByIdRequest(id));
            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(blogPost);
        }

        // POST api/<BlogPostsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRequest request)
        {
            var blogPost = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { id = blogPost.Id }, blogPost);
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateByIdRequest request)
        {
            var blogPost = await _mediator.Send(new UpdateByIdRequest(id, request.Title, request.Text, request.CreatedBy, request.Timestamp, request.Tags, request.Comments));
            if (blogPost == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
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
