using System.Windows.Input;
using System.Threading.Tasks;

namespace DotaProTracker.ViewModels
{
    public class TopBarViewModel : BaseViewModel
    {
        public ICommand GoToHomeCommand { get; }
        public ICommand GoToHeroesCommand { get; }
        public ICommand GoToSavedCommand { get; }
        public ICommand LogoutCommand { get; }

        public TopBarViewModel()
        {
            GoToHomeCommand = new Command(async () => await GoToHome());
            GoToHeroesCommand = new Command(async () => await GoToHeroes());
            GoToSavedCommand = new Command(async () => await GoToSaved());
            LogoutCommand = new Command(async () => await Logout());
        }

        private async Task GoToHome()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async Task GoToHeroes()
        {
            await Shell.Current.GoToAsync("//HeroesPage");
        }

        private async Task GoToSaved()
        {
            await Shell.Current.GoToAsync("//SavedPage");
        }

        private async Task Logout()
        {
            await Models.UserStore.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
} 