using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<MyTask> Tasks { get; set; } = new();

        public MainViewModel()
        {
            FillData();
        }

        //Sample data
        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category { Id = 1, CategoryName = "WAWA",   Color = "#cf14df" },
                new Category { Id = 2, CategoryName = "Meow",   Color = "#df6f14" },
                new Category { Id = 3, CategoryName = "Miming", Color = "#cf14df" }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask { TaskName = "Upload file",      Completed = false, CategoryId = 1 },
                new MyTask { TaskName = "Plan next",        Completed = false, CategoryId = 1 },
                new MyTask { TaskName = "Upload new video", Completed = false, CategoryId = 2 },
                new MyTask { TaskName = "Fix file",         Completed = false, CategoryId = 2 },
                new MyTask { TaskName = "Update file",      Completed = true,  CategoryId = 2 },
                new MyTask { TaskName = "Cook lunch",       Completed = false, CategoryId = 3 },
                new MyTask { TaskName = "Order",            Completed = false, CategoryId = 3 }
            };

            UpdateData();
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                // Filter tasks belonging to this category
                var tasks = Tasks.Where(t => t.CategoryId == c.Id);
                var completed = tasks.Where(t => t.Completed);
                var pending = tasks.Where(t => !t.Completed);

                // Update pending count and progress percentage
                c.PendingTasks = pending.Count();
                c.Percentage = tasks.Any() ? (float)completed.Count() / tasks.Count() : 0f;
            }

            foreach (var t in Tasks)
            {
                // Assign task color
                t.TaskColor = Categories
                    .FirstOrDefault(c => c.Id == t.CategoryId)?.Color ?? string.Empty;
            }
        }
    }
}