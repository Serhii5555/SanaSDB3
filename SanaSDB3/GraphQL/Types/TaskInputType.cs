using GraphQL.Types;

namespace SanaSDB3.GraphQL.Types
{
    public class TaskInputType : InputObjectGraphType
    {
        public TaskInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
            Field<BooleanGraphType>("completed");
            Field<DateTimeGraphType>("dueDate");
            Field<IntGraphType>("categoryId");
        }
    }
}
