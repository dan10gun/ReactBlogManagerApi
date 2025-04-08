using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBlogApp.Model;
using ReactBlogApp.ServiceLogic;

namespace ReactBlogApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogActions : ControllerBase
    {
        public readonly IBlogAppSL _blogAppSL;

        public BlogActions(IBlogAppSL blogAppSL)
        {
            _blogAppSL = blogAppSL;
        }

        [HttpGet]
        [Route("GetBlogs")]
        public async Task<IActionResult> GetBlogs([FromQuery] GetBlogRequest request)
        {
            GetBlogResponse response = new GetBlogResponse();
            try
            { 
                response = await _blogAppSL.GetBlog(request);
            }
            catch (Exception ex) {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromBody] Blog request)
        {
            try
            {
                BlogResponse response = await _blogAppSL.CreateBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


        [HttpPut]
        [Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromBody] Blog request)
        {
            try
            {
                BlogResponse response = await _blogAppSL.UpdateBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog([FromBody] DeleteBlogRequest request)
        {
            try
            {
                BlogResponse response = await _blogAppSL.DeleteBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                response = await _blogAppSL.Login(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

    }
}
