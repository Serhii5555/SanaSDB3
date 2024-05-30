using SanaSDB3.Models;

namespace SanaSDB3.Repositories
{
     public interface ITasksRepository
     {
         Task Create(Tasks task);
         Task<Tasks> GetById(int id);
         Task Update(Tasks task);
         Task DeleteById(int id);
         Task<IEnumerable<Tasks>> GetAll();
     }
}