using DotaProTracker.ViewModels;

namespace DotaProTracker.Views;

public partial class SavedPage : ContentPage
{
    private readonly SavedViewModel _viewModel;

    public SavedPage(SavedViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSavedHeroes();
    }
} 