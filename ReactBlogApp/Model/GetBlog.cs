
namespace ReactBlogApp.Model
{
    public class GetBlogRequest
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Favourite { get; set; }
        public bool Personal { get; set; }

    }

    public class GetBlogResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<Blog>? Posts { get; internal set; }
    }

    public class Blog
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class BlogResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class DeleteBlogRequest
    {
        public int PostId { get; set; } 
    }

    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        public List<LoginDetails>? Logins { get; internal set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class LoginDetails
    {
        public int UserId { get; set; }
    }

}
