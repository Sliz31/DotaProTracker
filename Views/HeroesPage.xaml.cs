using DotaProTracker.ViewModels;
using DotaProTracker.Models;
using Microsoft.Maui.Media;

namespace DotaProTracker.Views;

public partial class HeroesPage : ContentPage
{
    private readonly HeroesViewModel _viewModel;

    public HeroesPage(HeroesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        try
        {
            await _viewModel.LoadHeroesAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load heroes: {ex.Message}", "OK");
        }
    }

    private async void OnReadDescriptionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Hero hero)
        {
            try
            {
                if (string.IsNullOrEmpty(hero.FullDescription))
                {
                    await DisplayAlert("Error", "Hero description is not available.", "OK");
                    return;
                }

                // Debug info
                await DisplayAlert("Debug", hero.FullDescription, "OK");

                // Check platform
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    await DisplayAlert("Info", "Text-to-Speech is not supported on Windows. Showing description instead.", "OK");
                    return;
                }

                await TextToSpeech.SpeakAsync(hero.FullDescription);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to read description: {ex.Message}", "OK");
            }
        }
    }
}
