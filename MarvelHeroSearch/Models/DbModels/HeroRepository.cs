using System;
using System.Data;
using Dapper;
using MarvelHeroSearch.Models.Hero;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MarvelHeroSearch.Models.DbModels
{
    public class HeroRepository : IHeroRepository
    {
        private readonly IDbConnection _connection;

        public HeroRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Gets All Products: Using Dapper: From the Heroes table, Returns a Collection of Ienumerable<CharacterDB> //
        public IEnumerable<CharacterDb> GetAllHeroes()
        {
            return _connection.Query<CharacterDb>("SELECT * FROM Heroes h ORDER BY h.HeroDbId DESC;");
        }

        // Add a Character to the Heroes table//
        public void InsertHero(CharacterDb character)
        {
            _connection.Execute("INSERT INTO Heroes (CharacterId, CharacterName) VALUES (@id, @name);",
                 new { id = character.CharacterId, name = character.CharacterName });
        }


        // Delete a Character from the Heroes table where ID = ID //
        public void DeleteHero(Character character)
        {
            _connection.Execute("DELETE FROM Heroes WHERE CharacterId = @id", new { id = character.id });
        }

    }
}

