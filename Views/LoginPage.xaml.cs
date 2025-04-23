using DotaProTracker.Models;
using DotaProTracker.Services;
using Microsoft.Maui.Controls;
using DotaProTracker.ViewModels;
using DotaProTracker.Views;
using Microsoft.Maui.Storage;
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
        FirebaseService.Init();

        string nickname = NicknameEntry.Text;
        string password = PasswordEntry.Text;

        var person = await FirebaseService.AuthenticateUserAsync(nickname, password);

        if (person != null)
        {
            Preferences.Set("LoggedInNickname", person.Nickname);
            await Navigation.PushAsync(new HomePage(person.Nickname));
        }
        else
        {
            await DisplayAlert("Ошибка", "Неверный никнейм или пароль", "OK");
        }
    }

    // Переход на страницу регистрации
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}