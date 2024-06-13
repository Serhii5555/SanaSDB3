using SanaSDB3.Repositories;

namespace SanaSDB3.Factories
{
    public interface IRepositoryFactory
    {
        ICategoriesRepository GetCategoriesRepository();

        ITasksRepository GetTasksRepository();
    }
}
