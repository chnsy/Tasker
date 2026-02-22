using Tasker.MVVM.ViewModels;

namespace Tasker.MVVM.Views;

public partial class MainView : ContentPage
{
    private MainViewModel mainViewModel = new MainViewModel();

    public MainView()
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }

    private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is MainViewModel vm)
            vm.UpdateData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView(mainViewModel);
        Navigation.PushAsync(taskView);
    }
}