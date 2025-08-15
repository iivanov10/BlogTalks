using BlogTalks.Application.Contracts;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetAllResponse
    {
        public IEnumerable<BlogPostModel> BlogPosts { get; set; } = new List<BlogPostModel>();
        public Metadata Metadata { get; set; }
    }
}
