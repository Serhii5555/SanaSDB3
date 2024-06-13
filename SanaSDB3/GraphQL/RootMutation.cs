using GraphQL;
using GraphQL.Types;
using SanaSDB3.Factories;
using SanaSDB3.GraphQL.Types;
using SanaSDB3.Models;

namespace SanaSDB3.GraphQL
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(RepositoryResolver repositoryResolver)
        {
            Field<TaskType>("createTask")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<TaskInputType>> { Name = "task" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType" }
                ))
                .ResolveAsync(async context =>
                {
                    var taskInput = context.GetArgument<Tasks>("task");
                    var storageType = context.GetArgument<string>("storageType");
                    var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));
                    var repository = resolver.GetTasksRepository();
                    await repository.Create(taskInput);
                    return taskInput;
                });

            Field<BooleanGraphType>("deleteTask")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var storageType = context.GetArgument<string>("storageType");
                    var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));
                    var repository = resolver.GetTasksRepository();
                    await repository.DeleteById(id);
                    return true;
                });

            Field<TaskType>("completeTask")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "storageType" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var storageType = context.GetArgument<string>("storageType");
                    var resolver = repositoryResolver.SetStorageType(Enum.Parse<StorageType>(storageType));
                    var repository = resolver.GetTasksRepository();
                    var task = await repository.GetById(id);
                    if (task != null)
                    {
                        task.Completed = true;
                        await repository.Update(task);
                    }
                    return task;
                });
        }
    }
}
