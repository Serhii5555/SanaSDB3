using System.Collections.Generic;

namespace SanaSDB3
{
    public class TasksViewModel
    {
        public IEnumerable<Tasks> TaskList { get; set; }
        public Tasks NewTask { get; set; }
    }
}