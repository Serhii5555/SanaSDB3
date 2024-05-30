using Dapper;
using Microsoft.Data.SqlClient;
using SanaSDB3.Models;


namespace SanaSDB3.Repositories.SQLRepositories
{
    public class SQLTasksRepository : ITasksRepository
    {
        private string _connectionString;

        public SQLTasksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }

        public async Task Create(Tasks task)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "INSERT INTO Tasks (Name, Completed, DueDate, CategoryId) VALUES (@Name, @Completed, @DueDate, @CategoryId)",
                new { task.Name, task.Completed, task.DueDate, task.CategoryId }
            );
        }

        public async Task<Tasks> GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Tasks>(
                "SELECT * FROM Tasks WHERE Id = @id",
                new { id }
            );
        }

        public async Task Update(Tasks task)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UPDATE Tasks SET Name = @Name, Completed = @Completed, DueDate = @DueDate, CategoryId = @CategoryId WHERE Id = @Id",
                new { task.Name, task.Completed, task.DueDate, task.CategoryId, task.Id }
            );
        }

        public async Task DeleteById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "DELETE FROM Tasks WHERE Id = @id",
                new { id }
            );
        }

        public async Task<IEnumerable<Tasks>> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Tasks>("SELECT * FROM Tasks");
        }
    }
}
