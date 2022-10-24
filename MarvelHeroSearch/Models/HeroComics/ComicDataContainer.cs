using System;
namespace MarvelHeroSearch.Models.HeroComics
{
    public class ComicDataContainer
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public List<ComicBook> results { get; set; }
    }
}

