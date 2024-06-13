using SanaSDB3.Repositories.XMLRepositories;
using SanaSDB3.Repositories;

namespace SanaSDB3.Factories
{
    public class XMLFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    { 
        public ICategoriesRepository GetCategoriesRepository()
        {
            return serviceProvider.GetRequiredService<XMLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return serviceProvider.GetRequiredService<XMLTasksRepository>();
        }
    }
}
