using System;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.Hero;
using Xunit;
namespace MarvelHeroSearch.Tests
{
    public class MarvelApiClientTests
    {
        [InlineData("thor", "thor")]
        public void ShouldReturnCharacter(string name, string expected)
        {
            // Arrange //
            var marvelApi = new MarvelApiClient();
            // Act //
            var actual = marvelApi.GetCharacter(name);
            // Assert //
            Assert.Equal(expected, actual.data.results[0].name);
        }
    }
}

