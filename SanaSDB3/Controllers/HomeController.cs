using Microsoft.AspNetCore.Mvc;
using SanaSDB3.Models;
using SanaSDB3.Repositories;
using SanaSDB3.Repositories.SQLRepositories;
using System.Diagnostics;
using SanaSDB3.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using SanaSDB3.Factories;

namespace SanaSDB3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryResolver _repositoryResolver;

        public HomeController(ILogger<HomeController> logger, RepositoryResolver repositoryResolver)
        {
            _logger = logger;
            _repositoryResolver = repositoryResolver;
        }

        [HttpPost]
        public IActionResult ChangeStorageType(StorageType storageType)
        {
            _repositoryResolver.SetStorageType(storageType);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _repositoryResolver.GetTasksRepository().GetAll();
            var categories = await _repositoryResolver.GetCategoriesRepository().GetAll();

            var tasksList = new TaskViewModel
            {
                TaskList = tasks,
                CategoriesList = categories,
                StorageTypeSelected = _repositoryResolver.GetStorageType()
            };
            return View(tasksList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskCreate([Bind(Prefix = "NewTask")] Tasks model)
        {
            if (ModelState.IsValid)
            {
                var task = new Tasks
                {
                    Name = model.Name,
                    Completed = model.Completed,
                    DueDate = model.DueDate,
                    CategoryId = model.CategoryId
                };

                await _repositoryResolver.GetTasksRepository().Create(task);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate([Bind(Prefix = "NewCategory")] Categories model)
        {
            if (ModelState.IsValid)
            {
                var category = new Categories
                {
                    Name = model.Name,
                };

                await _repositoryResolver.GetCategoriesRepository().Create(category);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _repositoryResolver.GetTasksRepository().DeleteById(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskComplete(int Id)
        {
            var task = await _repositoryResolver.GetTasksRepository().GetById(Id);
            if (task.Completed)
            {
                task.Completed = false;
            }
            else
            {
                task.Completed = true;
            }

            await _repositoryResolver.GetTasksRepository().Update(task);
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}