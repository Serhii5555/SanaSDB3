using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using SanaSDB3.Factories;
using SanaSDB3.GraphQL.Types;
using SanaSDB3.Models;

namespace SanaSDB3.GraphQL
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(RepositoryResolver repositoryResolver)
        {
            Field<ListGraphType<TaskType>>("tasks")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType", Description = "Type of storage, for example: XML, SQL" },
                new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the task" }
            ))
            .ResolveAsync(async context =>
            {
                var storageType = context.GetArgument<string>("storageType");
                var id = context.GetArgument<int?>("id");
                var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));

                if (id.HasValue)
                {
                    var task = await resolver.GetTasksRepository().GetById(id.Value);
                    IEnumerable<Tasks> tasks = new List<Tasks> { task };
                    return tasks;
                }
                else
                {
                    var tasks = await resolver.GetTasksRepository().GetAll();
                    return tasks;
                }
            });


            Field<ListGraphType<CategoryType>>("categories")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType" }))
                .ResolveAsync(async context =>
                {
                    var storageType = context.GetArgument<string>("storageType");
                    var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));
                    var categories = await resolver.GetCategoriesRepository().GetAll();
                    return categories;
                });
        }
    }
}
