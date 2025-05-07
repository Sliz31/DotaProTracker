using DotaProTracker.Models;
using DotaProTracker.Services;
using Microsoft.Maui.Controls;
using DotaProTracker.ViewModels;
using DotaProTracker.Views;
using Microsoft.Maui.Storage;
using System;
using Microsoft.Extensions.Logging.Abstractions;
namespace DotaProTracker;

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
                Preferences.Set("LoggedInNickname", person.Nickname);
                await Navigation.PushAsync(new HomePage(person.Nickname));
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