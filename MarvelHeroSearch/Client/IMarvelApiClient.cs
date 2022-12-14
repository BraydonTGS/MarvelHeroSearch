using System;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Models.HeroComics;

namespace MarvelHeroSearch.Client
{
    public interface IMarvelApiClient
    {
        public CharacterDataWrapper GetCharacter(string characterName);

        public CharacterDataWrapper GetListOfCharacters();

        public CharacterDataWrapper GetCharacterById(string id);

        public ComicDataWrapper GetCharacterComics(string id);

    }
}

