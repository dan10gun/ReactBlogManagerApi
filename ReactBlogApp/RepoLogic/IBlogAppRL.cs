using ReactBlogApp.Model;

namespace ReactBlogApp.RepoLogic
{
    public interface IBlogAppRL
    {
        string GenerateQuery(GetBlogRequest request);
        string GenerateInputQuery(bool isUpdate);
        public Task<IEnumerable<Blog>> QueryDB(string query, object parameters);
        public Task<int> ExecuteQueryAsync(string query, object parameters);
        string GenerateDeleteQuery();
        string GenerateLoginQuery();
        public Task<IEnumerable<LoginDetails>> LoginDB(string query, object parameters);
    }
}
