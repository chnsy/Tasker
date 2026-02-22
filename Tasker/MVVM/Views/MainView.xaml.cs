using Tasker.MVVM.ViewModels;

namespace Tasker.MVVM.Views;

public partial class MainView : ContentPage
{
    private MainViewModel mainViewModel;

    public MainView()
    {
        // creates VM with data
        mainViewModel = new MainViewModel();
        // connects VM to UI
        BindingContext = mainViewModel;
        InitializeComponent();
    }

    private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        mainViewModel.UpdateData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView(mainViewModel);
        // navigates to NewTaskView
        Navigation.PushAsync(taskView);
    }
}