using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using DotaProTracker.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DotaProTracker.Services
{
    public class HeroService : IHeroService
    {
        private List<Hero> _cachedHeroes;

        public async Task<List<Hero>> GetHeroes()
        {
            if (_cachedHeroes != null)
            {
                Debug.WriteLine("HeroService: Returning cached heroes");
                return _cachedHeroes;
            }

            Debug.WriteLine("HeroService: Fetching heroes from API");
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.opendota.com/api/heroStats");
            _cachedHeroes = JsonConvert.DeserializeObject<List<Hero>>(json);
            return _cachedHeroes;
        }
    }
}

