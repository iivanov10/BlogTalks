using BlogTalks.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public CreateHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var blogPostDTO = new BlogPostDTO
            {
                Id = request.Id,
                Title = request.Title,
                Text = request.Text,
                CreatedBy = request.CreatedBy,
                Timestamp = request.Timestamp,
                Tags = request.Tags,
                Comments = request.Comments
            };

            await _fakeDataStore.CreateBlogPost(blogPostDTO);

            return new CreateResponse
            {
                Id = request.Id,
                Title = request.Title,
                Text = request.Text,
                CreatedBy = request.CreatedBy,
                Timestamp = request.Timestamp,
                Tags = request.Tags,
                Comments = request.Comments
            };
        }
    }
}
