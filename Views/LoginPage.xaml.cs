using DotaProTracker.Models;
using DotaProTracker.Services;
using Microsoft.Maui.Controls;
using DotaProTracker.ViewModels;
using System;

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
        // Получаем контекст ViewModel
        var viewModel = BindingContext as ViewModels.LoginPageViewModel;

        if (viewModel != null)
        {
            FirebaseService.Init();
            var person = await FirebaseService.AuthenticateUserAsync(viewModel.LoginInput, viewModel.Password);
            

            if (person != null)
            {
                // Успешный вход
                await DisplayAlert("Login", $"Welcome back, {person.Name}!", "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Ошибка: неверный email или пароль
                await DisplayAlert("Error", $"{person.Email} + {person.Password}", "OK");
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