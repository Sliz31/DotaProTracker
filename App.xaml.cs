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
    }
}
