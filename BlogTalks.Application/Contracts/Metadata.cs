namespace BlogTalks.Application.Contracts
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
