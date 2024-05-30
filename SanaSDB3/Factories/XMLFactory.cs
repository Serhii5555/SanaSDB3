using SanaSDB3.Factory;
using SanaSDB3.Repositories.XMLRepositories;
using SanaSDB3.Repositories;

namespace SanaSDB3.Factories
{
    public class XMLFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public XMLFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICategoriesRepository GetCategoriesRepository()
        {
            return _serviceProvider.GetRequiredService<XMLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return _serviceProvider.GetRequiredService<XMLTasksRepository>();
        }
    }
}
