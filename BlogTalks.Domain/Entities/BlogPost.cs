﻿using BlogTalks.Domain.Shared;

namespace BlogTalks.Domain.Entities
{
    public class BlogPost : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        // Navigation
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
