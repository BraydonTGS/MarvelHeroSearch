using System;
using MarvelHeroSearch.Models.HeroComics;

namespace MarvelHeroSearch.Models.Hero
{

    public class Character
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public CharacterImage? thumbnail { get; set; }
        public string resourceURI { get; set; }
        public ComicList comics { get; set; }
        public SeriesList series { get; set; }
        public StoryList stories { get; set; }
        public EventList events { get; set; }
        public List<CharacterUrl> urls { get; set; }
        public List<ComicBook> ComicBooks { get; set; } = new List<ComicBook>();


        public string GetImageLarge()
        {
            return $"{thumbnail.path}/{thumbnail.Landscape}.{thumbnail.extension}";
        }
        public string GetImage()
        {
            return $"{thumbnail.path}/{thumbnail.Portrait}.{thumbnail.extension}";
        }
    }



}

