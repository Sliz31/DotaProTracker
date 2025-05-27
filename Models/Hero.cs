using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DotaProTracker.Models
{
    public partial class Hero : ObservableObject
    {
        [JsonProperty("name")]
        public string RawName { get; set; }  // Пример: npc_dota_hero_tinker

        public string HeroId => RawName.Replace("npc_dota_hero_", "").ToLower();

        public string SteamImageUrl => $"https://cdn.cloudflare.steamstatic.com/apps/dota2/images/heroes/{HeroId}_full.png";

        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }

        [JsonProperty("primary_attr")]
        public string PrimaryAttr { get; set; }

        [JsonIgnore]
        public string PrimaryAttrReadable => PrimaryAttr switch
        {
            "str" => "Strength",
            "agi" => "Agility",
            "int" => "Intelligence",
            "all" => "Universal",
            _ => PrimaryAttr
        };

        [JsonProperty("attack_type")]
        public string AttackType { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonIgnore]
        public string FullImageUrl { get; set; }

        [JsonIgnore]
        public string DescriptionText =>
            $"{LocalizedName}. Primary attribute: {PrimaryAttrReadable}. Attack type: {AttackType}. Roles: {string.Join(", ", Roles)}.";

        [JsonIgnore]
        public string FullDescription { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        [ObservableProperty]
        private bool isFavorite;
    }
}
