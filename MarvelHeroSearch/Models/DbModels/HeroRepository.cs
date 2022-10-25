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
        public IEnumerable<CharacterDb> GetAllProducts()
        {
            return _connection.Query<CharacterDb>("SELECT * FROM Heroes h ORDER BY h.HeroDbId DESC;");
        }

        // Add a Character //



    }
}

