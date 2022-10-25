using System;
using MarvelHeroSearch.Models.Hero;

namespace MarvelHeroSearch.Models.DbModels
{
    public class CharacterDb
    {
        public int CharacterId { get; set; }
        public string? CharacterName { get; set; }
        public string? thumbnail { get; set; }
    }
}

