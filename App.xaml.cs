using Microsoft.Maui.Controls;
using DotaProTracker.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DotaProTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new AppShell();
            // MainPage = new Main();
            MainPage = new NavigationPage(new WelcomePage());
        }

        public static IServiceProvider Services { get; private set; }

        protected override void OnStart()
        {
            Services = MauiProgram.CreateMauiApp().Services;
        }
    }
}
