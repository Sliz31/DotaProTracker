using DotaProTracker.Services;
using DotaProTracker.ViewModels;
using DotaProTracker.Views;
using DotaProTracker.Models;

namespace DotaProTracker.Views
{
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }

    // Обработчик кнопки "Login"
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        FirebaseService.Init();

        if (BindingContext is LoginPageViewModel viewModel)
        {
            string loginInput = viewModel.LoginInput;
            string password = viewModel.Password;
            bool isEmail = viewModel.SelectedLoginMethodIndex == 0;

            var person = await FirebaseService.AuthenticateUserAsync(loginInput, password, isEmail);

            if (person != null)
            {
                    UserStore.CurrentUser = person;
                Preferences.Set("LoggedInNickname", person.Nickname);
                    await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверные данные", "OK");
            }
        }
    }

    // Переход на страницу регистрации
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
        }
    }
}