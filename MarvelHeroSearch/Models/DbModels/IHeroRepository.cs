using System;
using MarvelHeroSearch.Models.Hero;

namespace MarvelHeroSearch.Models.DbModels
{
    public interface IHeroRepository
    {
        // HeroRepository Implements IHeroRepository => Dependency Injection and Repo Pattern //
        public IEnumerable<CharacterDb> GetAllHeroes();
        public void InsertHero(CharacterDb productToInsert);
        public void DeleteHero(Character character);
    }
}

