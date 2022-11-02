using System;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Tests;
using Xunit;
namespace MarvelHeroSearch.Tests
{
    public class MarvelApiClientTests
    {
        // Test Character By Name Method //
        [Theory]
        [InlineData("Thor")]
        [InlineData("Scarlet Witch")]
        [InlineData("Iron Man")]
        [InlineData("Hulk")]
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


        // Test Character By Id Method //
        [Theory]
        [InlineData("1010716")]
        [InlineData("1011163")]
        [InlineData("1009739")]
        [InlineData("1010780")]
        public void ShouldReturnCharacterId(string id)
        {
            // Arrange //
            var marvelApi = new MarvelApiClient();
            // Act //
            var result = marvelApi.GetCharacterById(id);

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(id, result.data.results[0].id.ToString());


        }
    }
}



