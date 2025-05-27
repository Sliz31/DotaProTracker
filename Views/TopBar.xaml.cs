using DotaProTracker.Models;
using DotaProTracker.Services;
using DotaProTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DotaProTracker.Views;

public partial class TopBar : ContentView
{
    public event EventHandler? SavedClicked;
    public event EventHandler? SettingsClicked;

    private readonly TopBarViewModel _viewModel;

    public TopBar()
    {
        InitializeComponent();
        _viewModel = new TopBarViewModel();
        BindingContext = _viewModel;
        UpdateWelcomeMessage();
    }

    private void UpdateWelcomeMessage()
    {
        if (UserStore.CurrentUser != null)
        {
            WelcomeLabel.Text = $"Welcome, {UserStore.CurrentUser.Nickname}!";
        }
        else
        {
            WelcomeLabel.Text = string.Empty;
        }
    }

    private async void OnSavedClicked(object sender, EventArgs e)
    {
        if (Application.Current.MainPage is NavigationPage navigationPage)
        {
            var currentPage = navigationPage.CurrentPage;
            if (currentPage.GetType() != typeof(SavedPage))
            {
                var viewModel = App.Current.Handler.MauiContext.Services.GetService<SavedViewModel>();
                if (viewModel != null)
                {
                    await navigationPage.PushAsync(new SavedPage(viewModel));
                }
            }
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Settings", "Settings page coming soon!", "OK");
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        if (Application.Current.MainPage is NavigationPage navigationPage)
        {
            var currentPage = navigationPage.CurrentPage;
            if (currentPage.GetType() != typeof(HomePage))
            {
                await navigationPage.PushAsync(new HomePage());
            }
        }
    }

    private async void OnHeroesClicked(object sender, EventArgs e)
    {
        if (Application.Current.MainPage is NavigationPage navigationPage)
        {
            var currentPage = navigationPage.CurrentPage;
            if (currentPage.GetType() != typeof(HeroesPage))
            {
                var viewModel = App.Current.Handler.MauiContext.Services.GetService<HeroesViewModel>();
                if (viewModel != null)
                {
                    await navigationPage.PushAsync(new HeroesPage(viewModel));
                }
            }
        }
    }

    private async void OnMetaClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Meta", "Meta page coming soon!", "OK");
    }

    private async void OnPlayersClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Players", "Players page coming soon!", "OK");
    }
} 