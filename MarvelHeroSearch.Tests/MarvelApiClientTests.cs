using System;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Tests;
using Xunit;
namespace MarvelHeroSearch.Tests
{
    public class MarvelApiClientTests
    {
        [Theory]
        [InlineData("Thor")]
        public void ShouldReturnCharacterName(string characterName)
        {
            // Arrange //
            var marvelApi = new MarvelApiClient();
            // Act //
            var result = marvelApi.GetCharacter(characterName);

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(characterName, result.data.results[0].name);


        }
    }
}



