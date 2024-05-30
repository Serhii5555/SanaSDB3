using SanaSDB3.Repositories.SQLRepositories;
using SanaSDB3.Repositories;
using SanaSDB3.Factories;
using SanaSDB3.Repositories.XMLRepositories;
using SanaSDB3.Factory;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
