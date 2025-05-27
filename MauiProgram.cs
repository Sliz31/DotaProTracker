using Microsoft.Extensions.Logging;
using DotaProTracker.Services;
using DotaProTracker.ViewModels;
using DotaProTracker.Views;
using Firebase.Database;

namespace DotaProTracker;

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

        // Firebase initialization
        var firebaseClient = new FirebaseClient(
            "https://dotaprotacker-default-rtdb.firebaseio.com/",
            new FirebaseOptions()
        );
        builder.Services.AddSingleton(firebaseClient);

        builder.Services.AddSingleton<IHeroService, HeroService>();
        builder.Services.AddSingleton<FavoritesService>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegistrationPage>();
        builder.Services.AddTransient<RegistrationViewModel>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<HeroesPage>();
        builder.Services.AddSingleton<HeroesViewModel>();
        builder.Services.AddTransient<SavedPage>();
        builder.Services.AddSingleton<SavedViewModel>();
        builder.Services.AddTransient<TopBar>();
        builder.Services.AddTransient<TopBarViewModel>();

            return builder.Build();
    }
}
