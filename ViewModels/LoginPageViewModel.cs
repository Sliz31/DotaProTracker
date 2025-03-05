using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DotaProTracker.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string _loginInput = string.Empty; // Поле для ввода email или nickname
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _nickname = string.Empty;
        private int _selectedLoginMethodIndex; // Индекс выбранного метода (email или nickname)
        private string _loginPlaceholder = "Enter your email"; // Placeholder по умолчанию

        public ObservableCollection<string> LoginMethods { get; } = new ObservableCollection<string>
        {
            "Email",
            "Nickname"
        };

        public string LoginInput
        {
            get => _loginInput;
            set
            {
                _loginInput = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(); // Аналогично с паролем
            }
        }

        public int SelectedLoginMethodIndex
        {
            get => _selectedLoginMethodIndex;
            set
            {
                _selectedLoginMethodIndex = value;
                OnPropertyChanged();
                UpdateLoginPlaceholder(); // Обновляем подсказку при смене метода входа
            }
        }

        public string LoginPlaceholder
        {
            get => _loginPlaceholder;
            private set
            {
                _loginPlaceholder = value;
                OnPropertyChanged();
            }
        }

        // Обновляем подсказку в зависимости от выбранного метода входа
        private void UpdateLoginPlaceholder()
        {
            LoginPlaceholder = SelectedLoginMethodIndex == 0 ? "Enter your email" : "Enter your nickname";
        }

        // Это событие уведомляет интерфейс о том, что данные изменились
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}