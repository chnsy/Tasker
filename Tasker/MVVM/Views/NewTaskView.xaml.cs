using Tasker.MVVM.ViewModels;

namespace Tasker.MVVM.Views;

public partial class NewTaskView : ContentPage
{
    public NewTaskView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = new NewTaskViewModel(mainViewModel);
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}