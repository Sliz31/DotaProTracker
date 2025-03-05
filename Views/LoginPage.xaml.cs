using DotaProTracker.Models;

namespace DotaProTracker;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    // Обработчик кнопки "Login"
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Получаем контекст ViewModel
        var viewModel = BindingContext as ViewModels.LoginPageViewModel;

        if (viewModel != null)
        {
            // Получаем значение LoginInput
            var loginValue = viewModel.LoginInput;

            // Проверяем пользователя по email и паролю
             var person = UserStore.ValidateUser(loginValue, viewModel.Password);

            if (person != null)
            {
                // Успешный вход
                await DisplayAlert("Login", $"Welcome back, {person.Name}!", "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Ошибка: неверный email или пароль
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }
    }

    // Переход на страницу регистрации
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}