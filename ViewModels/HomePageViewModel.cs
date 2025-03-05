using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotaProTracker.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public string UserName { get; set; } = "(User)";
        public Command SavedCommand { get; }
        public Command SettingsCommand { get; }
        public Command<string> NavigateCommand { get; }

        public string Introduction => "Imagine an exhilarating world where every decision can change the course of the game, and teamwork and strategy are the keys to victory. This is Dota 2—one of the most popular and complex multiplayer online games of our time.";

        public string WhatIsDota => "Dota 2 is a free-to-play multiplayer online battle arena (MOBA) game developed by Valve Corporation. It is the standalone sequel to the original Defense of the Ancients (DotA) mod for Warcraft III.";

        public string Objective => "In Dota 2, two teams of five players—Radiant and Dire—battle against each other. The goal is to destroy the enemy's Ancient, located deep within their base.";

        public string GameplayBasics => "Hero Selection, Roles, Positions, Items, and Teamwork are vital to success. Heroes are categorized into Carries, Supports, Offlaners, Mid Laners, and Junglers.";

        public string BeginnersGuide => "Start with beginner-friendly heroes like Sniper or Lich, practice last-hitting, and use practice modes. Communication and teamwork are key.";

        public string Conclusion => "Dota 2 combines strategy, tactics, and teamwork. Master its depth, and you will enjoy one of the greatest competitive games ever.";

        public HomePageViewModel()
        {
            SavedCommand = new Command(OnSavedClicked);
            SettingsCommand = new Command(OnSettingsClicked);
            NavigateCommand = new Command<string>(OnNavigate);
        }

        private async void OnSavedClicked()
        {
            await Shell.Current.GoToAsync("SavedPage");
        }

        private async void OnSettingsClicked()
        {
            await Shell.Current.GoToAsync("SettingsPage");
        }

        private async void OnNavigate(string pageName)
        {
            if (!string.IsNullOrWhiteSpace(pageName))
            {
                await Shell.Current.GoToAsync(pageName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
