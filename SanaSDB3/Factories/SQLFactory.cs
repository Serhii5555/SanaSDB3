using Microsoft.Extensions.DependencyInjection;
using SanaSDB3.Repositories;
using SanaSDB3.Repositories.SQLRepositories;

namespace SanaSDB3.Factory
{
    public class SQLFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SQLFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICategoriesRepository GetCategoriesRepository()
        {
            return _serviceProvider.GetRequiredService<SQLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return _serviceProvider.GetRequiredService<SQLTasksRepository>();
        }
    }
}
