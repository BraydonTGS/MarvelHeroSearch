using System;
namespace MarvelHeroSearch.Models.HeroComics
{
    public class Characters
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public List<ComicItems> items { get; set; }
        public int returned { get; set; }
    }
}

