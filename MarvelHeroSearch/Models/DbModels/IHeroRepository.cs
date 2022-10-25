using System;
namespace MarvelHeroSearch.Models.DbModels
{
    public interface IHeroRepository
    {
        public IEnumerable<CharacterDb> GetAllHeroes();
        public void InsertHero(CharacterDb productToInsert);
    }
}

