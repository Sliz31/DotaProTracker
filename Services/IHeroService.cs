using DotaProTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotaProTracker.Services
{
    public interface IHeroService
    {
        Task<List<Hero>> GetHeroes();
    }
} 