using System;
namespace MarvelHeroSearch.Models.Hero
{
    public class SeriesList
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public List<ListItems> items { get; set; }
        public int returned { get; set; }
    }
}

