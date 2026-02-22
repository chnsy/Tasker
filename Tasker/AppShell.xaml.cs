using Tasker.MVVM.Views;

namespace Tasker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SplashView), typeof(SplashView));
        }
    }
}
