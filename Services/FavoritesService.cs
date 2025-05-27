using DotaProTracker.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaProTracker.Services
{
    public class FavoritesService
    {
        private readonly FirebaseClient _firebase;
        private const string FavoritesCollection = "favorites";

        public FavoritesService(FirebaseClient firebase)
        {
            _firebase = firebase;
        }

        public async Task<List<int>> GetUserFavorites(string userId)
        {
            try
            {
                var favorites = await _firebase
                    .Child(FavoritesCollection)
                    .Child(userId)
                    .OnceSingleAsync<UserFavorites>();

                return favorites?.FavoriteHeroIds ?? new List<int>();
            }
            catch
            {
                return new List<int>();
            }
        }

        public async Task UpdateUserFavorites(string userId, List<int> favoriteHeroIds)
        {
            var userFavorites = new UserFavorites
            {
                UserId = userId,
                FavoriteHeroIds = favoriteHeroIds
            };

            await _firebase
                .Child(FavoritesCollection)
                .Child(userId)
                .PutAsync(userFavorites);
        }

        public async Task AddFavoriteHero(string userId, int heroId)
        {
            var favorites = await GetUserFavorites(userId);
            if (!favorites.Contains(heroId))
            {
                favorites.Add(heroId);
                await UpdateUserFavorites(userId, favorites);
            }
        }

        public async Task RemoveFavoriteHero(string userId, int heroId)
        {
            var favorites = await GetUserFavorites(userId);
            if (favorites.Contains(heroId))
            {
                favorites.Remove(heroId);
                await UpdateUserFavorites(userId, favorites);
            }
        }
    }
} 