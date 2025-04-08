using System.Data;
using Dapper;
using Microsoft.Extensions.Hosting;
using ReactBlogApp.Model;

namespace ReactBlogApp.RepoLogic
{
    public class BlogAppRL : IBlogAppRL
    {
        private readonly IDbConnection _dbConnection;

        public BlogAppRL(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public string GenerateQuery(GetBlogRequest request)
        {
            if (request.Favourite)
            {
                string query = $"SELECT * FROM Post AS p JOIN Favourite AS f ON p.PostId = f.PostId where f.UserId = @UserId";
                if (!string.IsNullOrEmpty(request.Title))
                    query += $" AND p.Title LIKE @Title";
                if (!string.IsNullOrEmpty(request.Description))
                    query += $" AND p.Description LIKE @Description";
                return query;
            }
            else if (request.Personal)
            {
                string query = $"SELECT * FROM Post WHERE UserId = @UserId";
                if (!string.IsNullOrEmpty(request.Title))
                    query += $" AND Title LIKE @Title";
                if (!string.IsNullOrEmpty(request.Description))
                    query += $" AND Description LIKE @Description";
                return query;
            }
            else
            {
                string query = "SELECT * FROM Post";
                if (!string.IsNullOrEmpty(request.Title))
                    query += $" WHERE Title LIKE @Title";
                if (!string.IsNullOrEmpty(request.Description))
                    query += $" WHERE Description LIKE @Description";

                Console.WriteLine(query);

                return query;
            }

        }

        public string GenerateInputQuery(bool isUpdate)
        {
            return isUpdate
                ? "UPDATE Post SET UserId = @UserId, Title = @Title, Description = @Description WHERE PostId = @PostId"
                : "INSERT INTO Post (UserId, Title, Description) VALUES (@UserId, @Title, @Description)";
        }

        public string GenerateDeleteQuery()
        {
            
            string query = $"DELETE FROM Post WHERE PostId = @PostId";
            return query;
            
        }

        public async Task<IEnumerable<Blog>> QueryDB(string query, object parameters)
        {
            try
            {
                return await _dbConnection.QueryAsync<Blog>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Database operation failed.", ex);
            }

        }

        public async Task<int> ExecuteQueryAsync(string query, object parameters)
        {
            try
            {
                return await _dbConnection.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Database operation failed.", ex);
            }
        }

        public string GenerateLoginQuery()
        {
            string query = $"SELECT UserId FROM User WHERE Email = @Email AND Password = @Password";
            return query;
        }

        public async Task<IEnumerable<LoginDetails>> LoginDB(string query, object parameters)
        {
            try
            {
                return await _dbConnection.QueryAsync<LoginDetails>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Login failed.", ex);
            }

        }



    }




}
