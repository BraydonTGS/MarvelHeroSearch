using System;
using System.Data;
using Dapper;
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

        // Gets All Products: Using Dapper: From the BB DB, Returns a Collection of Ienumerable<Products> //
        public IEnumerable<CharacterDb> GetAllHeroes()
        {
            return _connection.Query<CharacterDb>("SELECT * FROM Heroes h ORDER BY h.HeroDbId DESC;");
        }

        // Add a Character //
        public void InsertHero(CharacterDb character)
        {
            _connection.Execute("INSERT INTO Heroes (CharacterId, CharacterName, CharacterImage) VALUES (@id, @name, @image);",
                 new { id = character.CharacterId, name = character.CharacterName, image = character.thumbnail });
        }



    }
}

