using BlogTalks.Application.User.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(new LoginResponse { Token = response.Token });
        }
    }
}
