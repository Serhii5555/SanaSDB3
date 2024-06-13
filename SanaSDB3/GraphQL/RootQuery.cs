using GraphQL;
using GraphQL.Types;
using SanaSDB3.Factories;
using SanaSDB3.GraphQL.Types;

namespace SanaSDB3.GraphQL
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(RepositoryResolver repositoryResolver)
        {
            Field<ListGraphType<TaskType>>("tasks")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType" }))
                .ResolveAsync(async context =>
                {
                    var storageType = context.GetArgument<string>("storageType");
                    var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));
                    var tasks = await resolver.GetTasksRepository().GetAll();
                    return tasks;
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
