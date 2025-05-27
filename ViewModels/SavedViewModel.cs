using CommunityToolkit.Mvvm.ComponentModel;
using DotaProTracker.Models;
using DotaProTracker.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;

namespace DotaProTracker.ViewModels
{
    public partial class SavedViewModel : BaseViewModel
    {
        private readonly FavoritesService _favoritesService;
        private readonly IHeroService _heroService;
        private bool _isInitialized;

        [ObservableProperty]
        private ObservableCollection<Hero> savedHeroes = new ObservableCollection<Hero>();

        [ObservableProperty]
        private bool isLoading;

        public ICommand LoadSavedHeroesCommand { get; }

        public SavedViewModel(FavoritesService favoritesService, IHeroService heroService)
        {
            Title = "Saved Heroes";
            _favoritesService = favoritesService;
            _heroService = heroService;

            LoadSavedHeroesCommand = new Command(async () => await LoadSavedHeroes());
        }

        public async Task LoadSavedHeroes()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsLoading = true;

            try
            {
                Debug.WriteLine("SavedViewModel: Loading saved heroes");
                var currentUser = await Models.UserStore.GetCurrentUser();
                if (currentUser != null)
                {
                    var favoriteIds = await _favoritesService.GetUserFavorites(currentUser.Email);
                    var allHeroes = await _heroService.GetHeroes();
                    var savedHeroes = allHeroes.Where(h => favoriteIds.Contains(h.Id)).ToList();

                    // Only update if the heroes have changed
                    if (!_isInitialized || !SavedHeroes.SequenceEqual(savedHeroes))
                    {
                        SavedHeroes.Clear();
                        foreach (var hero in savedHeroes)
                        {
                            SavedHeroes.Add(hero);
                            Debug.WriteLine($"SavedViewModel: Added hero {hero.LocalizedName}");
                        }
                        _isInitialized = true;
                    }
                }
                Debug.WriteLine($"SavedViewModel: Loaded {SavedHeroes.Count} heroes");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SavedViewModel: Error loading heroes: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsBusy = false;
                Debug.WriteLine("SavedViewModel: Loading completed");
            }
        }
    }
} 