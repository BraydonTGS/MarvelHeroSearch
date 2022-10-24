using System;
namespace MarvelHeroSearch.Models.HeroComics
{
    public class ComicDataWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public ComicDataContainer data { get; set; }
        public string etag { get; set; }

    }
}

