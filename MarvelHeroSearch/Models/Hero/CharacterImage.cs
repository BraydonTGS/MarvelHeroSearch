using System;
namespace MarvelHeroSearch.Models.Hero
{
    public class CharacterImage
    {
        public string path { get; set; }
        public string extension { get; set; }

        // Image Members for Not Found //
        private string _imageNotFound = "http://i.annihil.us/u/prod/marvel/i/mg/b/40/image_not_available";
        private string _imageGifNotFound = "https://i.annihil.us/u/prod/marvel/i/mg/f/60/4c002e0305708";
        public bool IsImageFound => !string.IsNullOrWhiteSpace(path) && path != _imageNotFound && path != _imageGifNotFound;

        // Image Attributes //
        public string Portrait = "portrait_incredible";
        public string Landscape = "detail";


    }
}

