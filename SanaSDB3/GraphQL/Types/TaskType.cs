using GraphQL.Types;
using SanaSDB3.Models;

namespace SanaSDB3.GraphQL.Types
{
    public class TaskType : ObjectGraphType<Tasks>
    {
        public TaskType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Completed);
            Field(x => x.DueDate, nullable: true);
            Field(x => x.CategoryId, nullable: true);
        }
    }
}