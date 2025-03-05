using DotaProTracker.Views;
using Microsoft.Maui.Controls;

namespace DotaProTracker
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Saved"
        private async void OnSavedClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Saved", "Saved page coming soon!", "OK");
        }

        // Обработчик для кнопки "Settings"
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Settings", "Settings page coming soon!", "OK");
        }

        // Обработчики для категорий
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Home", "You are already on the Home page.", "OK");
        }

        private async void OnHeroesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HeroesPage());
        }

        private async void OnMetaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MetaPage());
        }

        private async void OnPlayersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayersPage());
        }
    }
}
