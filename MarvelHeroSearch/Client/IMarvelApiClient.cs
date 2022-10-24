using System;
using MarvelHeroSearch.Models.Hero;

namespace MarvelHeroSearch.Client
{
    public interface IMarvelApiClient
    {
        public CharacterDataWrapper GetCharacter(string characterName);

        public CharacterDataWrapper GetListOfCharacters();

        //public ComicDataWrapper GetCharacterComics(string url);
    }
}

