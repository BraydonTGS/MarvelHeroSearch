using System;
namespace MarvelHeroSearch.Models.HeroComics
{
    public class ComicBook
    {
        public int id { get; set; }
        public int digitalId { get; set; }
        public string title { get; set; }
        public int issueNumber { get; set; }
        public string variantDescription { get; set; }
        public string description { get; set; }
        public string isbn { get; set; }
        public string upc { get; set; }
        public string diamondCode { get; set; }
        public string ean { get; set; }
        public string issn { get; set; }
        public string format { get; set; }
        public int pageCount { get; set; }
        public string resourceURI { get; set; }
        public List<ComicPrice> prices { get; set; }
        public ComicThumbnail thumbnail { get; set; }
        public List<ComicImage> images { get; set; }
        public Creators creators { get; set; }
        public Characters characters { get; set; }

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

