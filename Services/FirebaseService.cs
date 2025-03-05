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

        /// <summary>
        /// Инициализирует подключение к Firebase согласно новому гайду.
        /// </summary>
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
                //var authLink = await auth.CreateUserWithEmailAndPasswordAsync(person.Email, person.Password);
                //person.FireBaseID = authLink.User.Uid;
                client.Child("persons").PostAsync(person);

            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка входа: " + ex.Message);
            }
        }


        ///// <summary>
        ///// Выполняет вход пользователя по email и password.
        ///// </summary>
        //public static async Task<FirebaseAuthLink> LoginAsync(string email, string password)
        //{
        //    try
        //    {
        //        var authLink = await auth.SignInWithEmailAndPasswordAsync(email, password);
        //        return authLink;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ошибка входа: " + ex.Message);
        //    }
        //}

        /// <summary>
        ///// Регистрирует нового пользователя.
        ///// </summary>
        //public static async Task<FirebaseAuthLink> RegisterAsync(string email, string password)
        //{
        //    try
        //    {
        //        var authLink = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
        //        return authLink;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ошибка регистрации: " + ex.Message);
        //    }
        //}

        /// <summary>
        /// Получает все элементы (items) из базы данных.
        /// </summary>
        //public static async Task<List<Item>> GetAllItemsAsync()
        //{
        //    try
        //    {
        //        var itemsFromFB = await client.Child("items").OnceAsync<Item>();
        //        var items = itemsFromFB.Select(fbItm => new Item()
        //        {
        //            Id = fbItm.Object.Id,
        //            Name = fbItm.Object.Name,
        //            Description = fbItm.Object.Description,
        //            FirebaseKey = fbItm.Key
        //        }).ToList();

        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ошибка получения данных: " + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Добавляет новый элемент в базу данных.
        ///// </summary>
        //public static async Task AddItemAsync(Item newItem)
        //{
        //    try
        //    {
        //        var result = await client.Child("items").PostAsync(newItem);
        //        // Сохраняем сгенерированный ключ Firebase в свойство FirebaseKey элемента
        //        newItem.FirebaseKey = result.Key;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ошибка добавления элемента: " + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Удаляет указанный элемент из базы данных.
        ///// </summary>
        //public static async Task DeleteItemAsync(Item itemToDelete)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(itemToDelete.FirebaseKey))
        //            throw new Exception("FirebaseKey отсутствует");

        //        await client.Child("items").Child(itemToDelete.FirebaseKey).DeleteAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ошибка удаления элемента: " + ex.Message);
        //    }
        //}
    }
}
