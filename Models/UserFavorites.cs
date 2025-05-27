using System.Collections.Generic;

namespace DotaProTracker.Models
{
    public class UserFavorites
    {
        public string UserId { get; set; }
        public List<int> FavoriteHeroIds { get; set; } = new List<int>();
    }
} 