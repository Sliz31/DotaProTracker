using DotaProTracker.Models;

namespace DotaProTracker
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Login"
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Переход на страницу LoginPage
            await Navigation.PushAsync(new LoginPage());
        }

        // Обработчик для кнопки "Register"
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Переход на страницу WelcomePage
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
