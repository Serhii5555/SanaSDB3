using Microsoft.AspNetCore.Mvc.Rendering;
using SanaSDB3.Factories;

namespace SanaSDB3.Models.ViewModels
{
    public class TaskViewModel
    {
        public Tasks NewTask { get; set; }
        public Categories NewCategory { get; set; }
        public IEnumerable<Tasks> TaskList { get; set; }
        public IEnumerable<Categories> CategoriesList { get; set; }
        public StorageType StorageTypeSelected { get; set; }

    }
}
