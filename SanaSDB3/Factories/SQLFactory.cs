using Microsoft.Extensions.DependencyInjection;
using SanaSDB3.Repositories;
using SanaSDB3.Repositories.SQLRepositories;

namespace SanaSDB3.Factory
{
    public class SQLFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        public ICategoriesRepository GetCategoriesRepository()
        {
            return serviceProvider.GetRequiredService<SQLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return serviceProvider.GetRequiredService<SQLTasksRepository>();
        }
    }
}
