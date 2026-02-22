using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Tasker.MVVM.Models;
using Microsoft.Maui.Controls;

namespace Tasker.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewTaskViewModel
    {
        public string TaskName { get; set; } 
        public ObservableCollection<Category> Categories { get; set; } = new();

        public ICommand AddTaskCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }

        private readonly MainViewModel _mainViewModel;

        public NewTaskViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            Categories = new ObservableCollection<Category>(_mainViewModel.Categories);

            AddTaskCommand = new Command(AddTask);
            AddCategoryCommand = new Command(AddCategory);
        }

        private async void AddTask()
        {
            // Dapat naka select category and naay task name
            var selectedCategory = Categories.FirstOrDefault(c => c.IsSelected);
            if (selectedCategory == null || string.IsNullOrWhiteSpace(TaskName)) return;

            // Create new task with selected category info
            var newTask = new MyTask
            {
                TaskName = TaskName,
                Completed = false,
                CategoryId = selectedCategory.Id,
                TaskColor = selectedCategory.Color
            };

            // Add task
            _mainViewModel.Tasks.Add(newTask);
            _mainViewModel.UpdateData();

            // Reset form fields
            TaskName = string.Empty;
            foreach (var c in Categories) c.IsSelected = false;

            // Go back to MainView
            if (Application.Current?.Windows[0].Page is Page page)
                await page.Navigation.PopAsync();
        }

        private async void AddCategory()
        {
            Page? page = Application.Current?.Windows[0].Page;
            if (page is null) return;

            string? name = await page.DisplayPromptAsync(
                "New Category", "Enter category name:");
            if (string.IsNullOrWhiteSpace(name)) return;

            // Create category with a random color
            var newCategory = new Category
            {
                Id = _mainViewModel.Categories.Count + 1,
                CategoryName = name,
                Color = GetRandomColor()
            };

            Categories.Add(newCategory);
            _mainViewModel.Categories.Add(newCategory);
        }

        // Generate random hex color
        private string GetRandomColor()
        {
            var random = new Random();
            return string.Format("#{0:X2}{1:X2}{2:X2}",
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));
        }
    }
}