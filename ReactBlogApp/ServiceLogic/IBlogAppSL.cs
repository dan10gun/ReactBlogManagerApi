using ReactBlogApp.Model;

namespace ReactBlogApp.ServiceLogic
{
    public interface IBlogAppSL
    {
        public Task<BlogResponse> CreateBlog(Blog request);
        public Task<BlogResponse> DeleteBlog(DeleteBlogRequest request);
        public Task<GetBlogResponse> GetBlog(GetBlogRequest request);
        public Task<BlogResponse> UpdateBlog(Blog request);
        public Task<LoginResponse> Login(LoginRequest request);
    }
}
