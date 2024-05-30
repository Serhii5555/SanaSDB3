using SanaSDB3.Repositories;

namespace SanaSDB3.Factory
{
    public interface IRepositoryFactory
    {
        ICategoriesRepository GetCategoriesRepository();

        ITasksRepository GetTasksRepository();
    }
}
