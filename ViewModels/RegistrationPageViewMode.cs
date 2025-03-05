﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DotaProTracker.ViewModels
{
    public class RegistrationPageViewModel : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _nickname = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string? _gender;
        private bool? _isUserNameValid;
        private bool? _isNicknameValid;
        private bool? _isPasswordValid;        
        private bool? _isPasswordConfirmed;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ValidateUserName(); // Проверка имени пользователя
                ValidateForm();     // Проверка всей формы
            }
        }

        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                OnPropertyChanged();
                ValidateNickname(); // Проверка никнейма
                ValidateForm();     // Проверка всей формы
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidatePassword(); // Добавь вызов этого метода для проверки пароля при вводе
                ValidateForm();     // Обновляем форму
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword; 
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ValidatePasswordConfirmation(); // Проверка совпадения паролей
                ValidateForm(); // Проверка всей формы
            }
        }

        public bool? IsUserNameValid
        {
            get => _isUserNameValid;
            set
            {
                if (_isUserNameValid != value)
                {
                    _isUserNameValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsNicknameValid
        {
            get => _isNicknameValid;
            set
            {
                if (_isNicknameValid != value)
                {
                    _isNicknameValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsPasswordValid
        {
            get => _isPasswordValid;
            set
            {
                if (_isPasswordValid != value)
                {
                    _isPasswordValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsPasswordConfirmed
        {
            get => _isPasswordConfirmed;
            set
            {
                if (_isPasswordConfirmed != value)
                {
                    _isPasswordConfirmed = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                IsUserNameValid = null; // Если поле пустое, то оставляем как "не проверено"
            }
            else if (Regex.IsMatch(Name, "^[a-zA-Z0-9]{3,15}$"))
            {
                IsUserNameValid = true;
            }
            else
            {
                IsUserNameValid = false;
            }
        }

        private void ValidateNickname()
        {
            if (string.IsNullOrWhiteSpace(Nickname))
            {
                IsNicknameValid = null; // Если поле пустое, то оставляем как "не проверено"
            }
            else if (Regex.IsMatch(Nickname, "^[a-zA-Z0-9]{3,15}$"))
            {
                IsNicknameValid = true;
            }
            else
            {
                IsNicknameValid = false;
            }
        }

        public void ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                IsPasswordValid = null; // Если поле пустое, то оставляем как "не проверено"
            }
            else if (Regex.IsMatch(Password, "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")) // Минимум 8 символов; 1 буква; 1 цифра
            {
                IsPasswordValid = true;
            }
            else
            {
                IsPasswordValid = false;
            }
        }

        public void ValidatePasswordConfirmation()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                IsPasswordConfirmed = null; // Если поле пустое, то оставляем как "не проверено"
            }
            else
            {
                // Устанавливаем true, если оба пароля совпадают
                IsPasswordConfirmed = Password == ConfirmPassword;
            }
        }

        public bool IsFormValid => (IsUserNameValid == true) && (IsNicknameValid == true) && (IsPasswordValid == true) && (IsPasswordConfirmed == true);

        private void ValidateForm()
        {
            // Проверка всей формы и вызов OnPropertyChanged для IsFormValid
            OnPropertyChanged(nameof(IsFormValid));
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

        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}