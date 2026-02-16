using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel() 
        {
            FillData();
        }

        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "WAWA",
                    Color = "#cf14df"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Meow",
                    Color = "#df6f14"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Miming",
                    Color = "#cf14df"
                }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Upload file",
                    Completed = false,
                    CategoryId = 1
                },
                new MyTask
                {
                    TaskName = "Plan next",
                    Completed = false,
                    CategoryId = 1
                },
                new MyTask
                {
                    TaskName = "Upload new video",
                    Completed = false,
                    CategoryId = 2
                },
                new MyTask
                {
                    TaskName = "Fix file",
                    Completed = false,
                    CategoryId = 2
                },
                new MyTask
                {
                    TaskName = "Update file",
                    Completed = true,
                    CategoryId = 2
                },
                new MyTask
                {
                    TaskName = "Cook lunch",
                    Completed = false,
                    CategoryId = 3
                },
                new MyTask
                {
                    TaskName = "Order ",
                    Completed = false,
                    CategoryId = 3
                }
            };

            UpdateData();
        }

        public void UpdateData()
        {
            foreach( var c in Categories)
            {
                //link to object
                var tasks = from t in Tasks
                           where t.CategoryId == c.Id
                           select t;

                var completed = from t in tasks
                               where t.Completed == true
                               select t;
                var notComplete = from t in tasks
                                  where t.Completed == false
                                  select t;
                c.PendingTasks = notComplete.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (from c in Categories
                     where c.Id == t.CategoryId
                     select c.Color).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}
