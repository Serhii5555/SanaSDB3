using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphQL;
using SanaSDB3.Factories;
using SanaSDB3.GraphQL;
using SanaSDB3.Repositories.SQLRepositories;
using SanaSDB3.Repositories.XMLRepositories;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.GraphiQL;
using GraphiQl;
using SanaSDB3.GraphQL.Types;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services
    .AddSingleton<SQLTasksRepository>()
    .AddSingleton<XMLTasksRepository>();

builder.Services
    .AddSingleton<SQLCategoriesRepository>()
    .AddSingleton<XMLCategoriesRepository>();

builder.Services.AddSingleton<SQLFactory>();
builder.Services.AddSingleton<XMLFactory>();
builder.Services.AddSingleton<RepositoryResolver>();

builder.Services.AddScoped<IRepositoryFactory, RepositoryResolver>();
builder.Services.AddSingleton<RootQuery>();
builder.Services.AddSingleton<RootMutation>();
builder.Services.AddSingleton<TaskInputType>();
builder.Services.AddSingleton<TaskType>(); 
builder.Services.AddSingleton<CategoryType>(); 
builder.Services.AddSingleton<ISchema, RootSchema>();

builder.Services.AddGraphQL(options =>
{
    options.AddSystemTextJson();
    options.AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseGraphQL<ISchema>();
app.UseGraphiQl("/graphiql");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
