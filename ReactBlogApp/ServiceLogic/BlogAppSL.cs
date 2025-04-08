using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Utilities;
using ReactBlogApp.Model;
using ReactBlogApp.RepoLogic;

namespace ReactBlogApp.ServiceLogic
{
    public class BlogAppSL : IBlogAppSL
    {
        public readonly IBlogAppRL _blogAppRL;

        public BlogAppSL(IBlogAppRL blogAppRL)
        {
            _blogAppRL = blogAppRL;
        }

        public async Task<GetBlogResponse> GetBlog(GetBlogRequest request)
        {
            if(request.Personal == true && request.Favourite == true)
            {
                var response = new GetBlogResponse()
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Posts = []
                };
                return response;
            }
            if(!string.IsNullOrEmpty(request.Description) && !string.IsNullOrEmpty(request.Title))
            {
                var response = new GetBlogResponse()
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Posts = []
                };
                return response;
            }
            try { 
                var parameters = new
                {
                    request.UserId,
                    Title = $"%{request.Title}%",
                    Description = $"%{request.Description}%"

                };
                string query = _blogAppRL.GenerateQuery(request);
                var posts = await _blogAppRL.QueryDB(query, parameters);

                var response = new GetBlogResponse()
                {
                    IsSuccess = true,
                    Posts = posts.ToList(),
                    Message = "Query executed successfully"
                };

                return response;
            }catch (Exception ex)
            {
                return new GetBlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<BlogResponse> CreateBlog(Blog request)
        {
            if(string.IsNullOrEmpty(request.Description) || string.IsNullOrEmpty(request.Title))
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = "Description and Title fields are required"
                };
            }
            if(request.UserId < 1)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                };
            }

            try
            {
                bool isUpdate = request.PostId > 0;
                string query = _blogAppRL.GenerateInputQuery(isUpdate);

                var parameters = new
                {
                    request.PostId,
                    request.UserId,
                    request.Title,
                    request.Description
                };

                int rowsAffected = await _blogAppRL.ExecuteQueryAsync(query, parameters);

                return new BlogResponse
                {
                    IsSuccess = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "Query executed successfully" : "No rows affected"
                };
            }
            catch (Exception ex)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<BlogResponse> UpdateBlog(Blog request)
        {
            if (string.IsNullOrEmpty(request.Description) || string.IsNullOrEmpty(request.Title))
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = "Description and Title fields are required"
                };
            }
            if (request.PostId < 1)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                };
            }

            try
            {
                bool isUpdate = request.PostId > 0;
                string query = _blogAppRL.GenerateInputQuery(isUpdate);

                var parameters = new
                {
                    request.PostId,
                    request.UserId,
                    request.Title,
                    request.Description
                };

                int rowsAffected = await _blogAppRL.ExecuteQueryAsync(query, parameters);

                return new BlogResponse
                {
                    IsSuccess = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "Query executed successfully" : "No rows affected"
                };
            }
            catch (Exception ex)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<BlogResponse> DeleteBlog(DeleteBlogRequest request)
        {
            if (request.PostId < 1)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                };
            }

            try
            {
                string query = _blogAppRL.GenerateDeleteQuery();

                var parameters = new
                {
                    request.PostId,
                };

                int rowsAffected = await _blogAppRL.ExecuteQueryAsync(query, parameters);

                return new BlogResponse
                {
                    IsSuccess = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "Query executed successfully" : "No rows affected"
                };
            }
            catch (Exception ex)
            {
                return new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {

            if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Password))
            {
                var response = new LoginResponse()
                {
                    IsSuccess = false,
                    Message = "Email and request fields are required",
                };
                return response;
            }
            try
            {
                var parameters = new
                {
                    request.Password,
                    request.Email
             

                };
                string query = _blogAppRL.GenerateLoginQuery();
                var logs = await _blogAppRL.LoginDB(query, parameters);

                var response = new LoginResponse()
                {
                    Logins = logs.ToList(),
                    IsSuccess = logs.ToList().Count == 1 ? true: false,
                    Message = logs.ToList().Count == 1 ? "Login successfully" : "Login failed",
                };
                Console.WriteLine(response.Message);
                return response;
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }



    }
}
