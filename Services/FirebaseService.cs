using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotaProTracker.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;

namespace DotaProTracker.Services
{
    public class FirebaseService
    {
        // Статические переменные для работы с аутентификацией и базой данных
        private static FirebaseAuthClient auth;
        private static FirebaseClient client;

        public static void Init()
        {
            var config = new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyCwJtbnGshK-fWQktwy8tIhH6-6ulfTohI",  // Новый API ключ
                AuthDomain = "https://dotaprotacker-default-rtdb.firebaseio.com/",           // Новый домен аутентификации
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider() // Используем аутентификацию по email
                },
            };

            auth = new FirebaseAuthClient(config);
            client = new FirebaseClient(
                "https://dotaprotacker-default-rtdb.firebaseio.com/", // URL базы данных
                new FirebaseOptions()
                );
        }


        public static async Task AddUserAsync(Person person)
        {
            try
            {
                client.Child("persons").PostAsync(person);

            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка входа: " + ex.Message);
            }
        }

        public static async Task<Person?> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                // Проверка, инициализирован ли клиент Firebase
                if (client == null)
                {
                    throw new Exception("Firebase client не инициализирован. Убедитесь, что вызван FirebaseServices.Init().");
                }

                var usersSnapshot = await client.Child("persons").OnceAsync<Person>();

                // Проверка, получены ли данные
                if (usersSnapshot == null)
                {
                    throw new Exception("Не удалось получить данные пользователей из Firebase.");
                }

                var user = usersSnapshot.Select(u => u.Object).FirstOrDefault(u => u.Email == email);

                if (user != null && user.Password == password)
                {
                    return user;
                }

                return null; // Если пользователь не найден или пароль неверный
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при аутентификации: " + ex.Message);
            }
        }
    }
}
