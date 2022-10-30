using System;
using MarvelHeroSearch.Models.Hero;

namespace MarvelHeroSearch.Models.DbModels
{
    public interface IHeroRepository
    {
        public IEnumerable<CharacterDb> GetAllHeroes();
        public void InsertHero(CharacterDb productToInsert);
        public void DeleteHero(Character character);
    }
}

