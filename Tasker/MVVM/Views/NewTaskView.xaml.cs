using Tasker.MVVM.ViewModels;

namespace Tasker.MVVM.Views;

public partial class NewTaskView : ContentPage
{
    public NewTaskView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = new NewTaskViewModel(mainViewModel);
    }

    // Back to MainView
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}