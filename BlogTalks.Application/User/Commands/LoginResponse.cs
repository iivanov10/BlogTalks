namespace BlogTalks.Application.User.Commands
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
