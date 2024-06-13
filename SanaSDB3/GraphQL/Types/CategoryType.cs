using GraphQL.Types;
using SanaSDB3.Models;

namespace SanaSDB3.GraphQL.Types
{
    public class CategoryType : ObjectGraphType<Categories>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}