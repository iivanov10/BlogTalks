namespace BlogTalks.Domain.DTOs
{
    public class FakeDataStore
    {
        private static List<CommentDTO> _comments;
        private static List<BlogPostDTO> _blogPosts;
        
        public FakeDataStore()
        {
            _comments = new List<CommentDTO>
            {
                new CommentDTO { Id = 1, Text = "This is the first comment", CreatedAt = DateTime.Now, CreatedBy = 1, BlogPostId = 1 },
                new CommentDTO { Id = 2, Text = "This is the second comment", CreatedAt = DateTime.Now, CreatedBy = 2, BlogPostId = 1 }
            };
            _blogPosts = new List<BlogPostDTO>
            {
                new BlogPostDTO { Id = 1, Title = "First Post", Text = "This is the first post", CreatedBy = 1, Timestamp = DateTime.Now, Tags = new List<string> { "tag1", "tag2" }, Comments = _comments },
                new BlogPostDTO { Id = 2, Title = "Second Post", Text = "This is the second post", CreatedBy = 2, Timestamp = DateTime.Now, Tags = new List<string> { "tag3" }, Comments = _comments }
            };
        }

        public async Task CreateComment(CommentDTO comment)
        {
            _comments.Add(comment);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllComments()
        {
            return await Task.FromResult(_comments);
        }

        public async Task<CommentDTO> GetCommentById(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(comment);
        }

        public async Task DeleteComment(int id)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            
            _comments.Remove(comment);
            await Task.FromResult(comment);
        }

        public async Task UpdateComment(int id, CommentDTO commentDTO)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == id);

            comment.Text = commentDTO.Text;
            comment.CreatedBy = commentDTO.CreatedBy;
            comment.CreatedAt = commentDTO.CreatedAt;
            comment.BlogPostId = commentDTO.BlogPostId;

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<CommentDTO>> GetByBlogPostId(int blogPostId)
        {
            var comments = _comments.Where(c => c.BlogPostId == blogPostId);
            return comments;
        }

        public async Task<IEnumerable<BlogPostDTO>> GetAllBlogPosts()
        {
            return await Task.FromResult(_blogPosts);
        }

        public async Task<BlogPostDTO> GetBlogPostById(int id)
        {
            var blogPost = _blogPosts.FirstOrDefault(bp => bp.Id == id);
            return await Task.FromResult(blogPost);
        }

        public async Task CreateBlogPost(BlogPostDTO blogPost)
        {
            _blogPosts.Add(blogPost);
            await Task.CompletedTask;
        }

        public async Task UpdateBlogPost(int id, BlogPostDTO blogPostDTO)
        {
            var blogPost = _blogPosts.FirstOrDefault(bp => bp.Id == id);
            
            blogPost.Title = blogPostDTO.Title;
            blogPost.Text = blogPostDTO.Text;
            blogPost.CreatedBy = blogPostDTO.CreatedBy;
            blogPost.Timestamp = blogPostDTO.Timestamp;
            blogPost.Tags = blogPostDTO.Tags;
            blogPost.Comments = blogPostDTO.Comments;

            await Task.CompletedTask;
        }

        public async Task DeleteBlogPost(int id)
        {
            var blogPost = _blogPosts.FirstOrDefault(bp => bp.Id == id);

            _blogPosts.Remove(blogPost);
            await Task.FromResult(blogPost);
        }
    }
}