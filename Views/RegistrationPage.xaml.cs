using DotaProTracker.Models;
using DotaProTracker.Services;

namespace DotaProTracker
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
  
        }

        // Обработчик события выбора пола
        private void OnGenderCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Проверяем, была ли радиокнопка выбрана
            if (e.Value)
            {
                var radioButton = (RadioButton)sender;
                var viewModel = BindingContext as ViewModels.RegistrationPageViewModel;

                if (viewModel != null)
                {
                    // Обновляем значение пола в ViewModel
                    if (radioButton.Content.ToString() == "Male")
                    {
                        viewModel.Gender = "Male";
                    }
                    else if (radioButton.Content.ToString() == "Female")
                    {
                        viewModel.Gender = "Female";
                    }
                    else
                    {
                        viewModel.Gender = "I'd rather not answer";
                    }
                }
            }
        }

        // Обработчик для регистрации
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Получаем данные из ViewModel
            var viewModel = BindingContext as ViewModels.RegistrationPageViewModel;

            if (viewModel != null)
            {
                // Создаем объект Person с данными из ViewModel
                var person = new Person
                {
                    Name = viewModel.Name,
                    Nickname = viewModel.Nickname,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Gender = viewModel.Gender
                };

                // Сохраняем пользователя
                UserStore.AddUser(person);
                FirebaseService.Init();
                FirebaseService.AddUserAsync(person);

                // Сообщаем об успешной регистрации
                await DisplayAlert("Registration", "You have been successfully registered!", "OK");

                // Переходим на страницу входа
                await Navigation.PushAsync(new LoginPage());
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
