using System;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.Hero;
using Xunit;
namespace MarvelHeroSearch.Tests
{
    public class MarvelApiClientTests
    {
        [Theory]
        [InlineData("thor", "thor")]
        public void ShouldReturnCharacterName(string characterName, string expected)
        {
            // Arrange //
            var marvelApi = new MarvelApiClient();
            // Act //
            var actual = marvelApi.GetCharacter(characterName).data.results[0].name;
            // Assert //
            Assert.Equal(expected, actual);
        }
    }
}

