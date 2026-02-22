namespace Tasker.MVVM.Views;

public partial class SplashView : ContentPage
{
    public SplashView()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RunAnimations();
    }

    private async Task RunAnimations()
    {
        // Fade in icon
        await IconLabel.FadeTo(1, 500);

        // Fade in title
        await TitleLabel.FadeTo(1, 500);

        // Fade in tagline
        await SubLabel.FadeTo(1, 400);

        // Hold for a moment
        await Task.Delay(800);

        // Scale up before navigating
        await Task.WhenAll(
            IconLabel.ScaleTo(1.2, 200),
            TitleLabel.ScaleTo(1.2, 200)
        );

        // Navigate to MainView and remove splash from back stack
        var mainView = new MainView();
        Navigation.InsertPageBefore(mainView, this);
        await Navigation.PopAsync();
    }
}