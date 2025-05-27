using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DotaProTracker.Models;
using Newtonsoft.Json;
using DotaProTracker.Services;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace DotaProTracker.ViewModels
{
    public partial class HeroesViewModel : BaseViewModel
    {
        private readonly IHeroService _heroService;
        private readonly FavoritesService _favoritesService;
        private bool _isBusy;
        private bool _isInitialized;

        [ObservableProperty]
        private ObservableCollection<Hero> heroes = new ObservableCollection<Hero>();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private ObservableCollection<int> savedHeroIds = new ObservableCollection<int>();

        public ICommand ToggleFavoriteCommand { get; }
        public ICommand LoadHeroesCommand { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public HeroesViewModel(IHeroService heroService, FavoritesService favoritesService)
        {
            Title = "Heroes";
            _heroService = heroService;
            _favoritesService = favoritesService;

            LoadHeroesCommand = new Command(async () => await LoadHeroes());
            ToggleFavoriteCommand = new Command<Hero>(async (hero) => await ToggleFavorite(hero));
        }

        public async Task LoadHeroesAsync()
        {
            if (_isInitialized) return;
            
            Debug.WriteLine("HeroesViewModel: LoadHeroesAsync started");
            
            try
            {
                IsLoading = true;

                // Load favorites first
                await LoadSavedHeroes();
                
                Debug.WriteLine("HeroesViewModel: Loading heroes from API");
                var heroList = await _heroService.GetHeroes();

                Debug.WriteLine($"HeroesViewModel: Loaded {heroList?.Count ?? 0} heroes from API");

                Heroes.Clear();
                if (heroList != null)
                {
                    foreach (var hero in heroList)
                    {
                        hero.FullImageUrl = hero.SteamImageUrl;
                        
                        if (!await UrlExists(hero.FullImageUrl))
                        {
                            Debug.WriteLine($"HeroesViewModel: Skipping hero {hero.LocalizedName} due to missing image");
                            continue;
                        }

                        var formattedDescription = $"Hero: {hero.LocalizedName}\n" +
                                                 $"Primary Attribute: {hero.PrimaryAttrReadable}\n" +
                                                 $"Attack Type: {hero.AttackType}\n" +
                                                 $"Roles: {string.Join(", ", hero.Roles)}\n\n" +
                                                 $"{hero.DescriptionText}";
                        
                        hero.FullDescription = formattedDescription;
                        hero.IsFavorite = SavedHeroIds.Contains(hero.Id);
                        
                        Heroes.Add(hero);
                        Debug.WriteLine($"HeroesViewModel: Added hero {hero.LocalizedName}");
                    }
                }

                _isInitialized = true;
                Debug.WriteLine("HeroesViewModel: Load completed successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HeroesViewModel: Error loading heroes: {ex.Message}");
                Debug.WriteLine($"HeroesViewModel: Stack trace: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
                Debug.WriteLine("HeroesViewModel: LoadHeroesAsync completed");
            }
        }

        private async Task LoadHeroes()
        {
            if (IsBusy || _isInitialized)
                return;

            await LoadHeroesAsync();
        }

        private async Task LoadSavedHeroes()
        {
            var currentUser = await Models.UserStore.GetCurrentUser();
            if (currentUser != null)
            {
                SavedHeroIds.Clear();
                var favorites = await _favoritesService.GetUserFavorites(currentUser.Email);
                foreach (var id in favorites)
                {
                    SavedHeroIds.Add(id);
                }
            }
        }

        private async Task ToggleFavorite(Hero hero)
        {
            var currentUser = await Models.UserStore.GetCurrentUser();
            if (currentUser == null)
                return;

            try
            {
                if (SavedHeroIds.Contains(hero.Id))
                {
                    await _favoritesService.RemoveFavoriteHero(currentUser.Email, hero.Id);
                    SavedHeroIds.Remove(hero.Id);
                    hero.IsFavorite = false;
                }
                else
                {
                    await _favoritesService.AddFavoriteHero(currentUser.Email, hero.Id);
                    SavedHeroIds.Add(hero.Id);
                    hero.IsFavorite = true;
                }

                // Notify SavedViewModel to refresh
                if (Application.Current?.Handler?.MauiContext != null)
                {
                    var savedViewModel = Application.Current.Handler.MauiContext.Services.GetService<SavedViewModel>();
                    if (savedViewModel != null)
                    {
                        await savedViewModel.LoadSavedHeroes();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HeroesViewModel: Error toggling favorite: {ex.Message}");
            }
        }

        private async Task<bool> UrlExists(string url)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}


