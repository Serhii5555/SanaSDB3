using SanaSDB3.Models;

namespace SanaSDB3.Repositories
{
    public interface ICategoriesRepository
    {
        Task Create(Categories category);
        Task<Categories> GetById(int id);
        Task Update(Categories category);
        Task DeleteById(int id);
        Task<IEnumerable<Categories>> GetAll();
    }
}
