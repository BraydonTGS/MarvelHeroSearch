using System;
using System.Text.Json;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Models.HeroComics;
using Newtonsoft.Json.Linq;

namespace MarvelHeroSearch.Client
{
    // ToDo Set up Exception Handling For my Api Calls - Morbius //
    public class MarvelApiClient : IMarvelApiClient
    {
        private const int _maxCharacters = 1562;
        private const string _limit = "100";

        private string _settings { get; set; }
        string _publicKey { get; set; }
        string _privateKey { get; set; }
        string _md5Hash { get; set; }

        public MarvelApiClient()
        {
            _settings = _settings = File.ReadAllText("appsettings.json");
            _publicKey = JObject.Parse(_settings).GetValue("PublicKey").ToString();
            _privateKey = JObject.Parse(_settings).GetValue("PrivateKey").ToString();
            _md5Hash = JObject.Parse(_settings).GetValue("Hash").ToString();

        }

        // Marvel API Client//
        HttpClient client = new HttpClient();

        // Get Character by Name //
        public CharacterDataWrapper GetCharacter(string characterName)
        {
            // Assemble The URL //
            string url = $"https://gateway.marvel.com:443/v1/public/characters?name={characterName}&ts=1&apikey={_publicKey}&hash={_md5Hash}";

            // Response -> String -> JSON -> Deserialize //
            var repo = client.GetStringAsync(url).Result;
            var response = JsonSerializer.Deserialize<CharacterDataWrapper>(repo);

            return response;
        }

        // Get Character by Id //
        public CharacterDataWrapper GetCharacterById(string id)
        {

            // Assemble The URL //
            string url = $"https://gateway.marvel.com/v1/public/characters/{id}?&ts=1&apikey={_publicKey}&hash={_md5Hash}";

            // Response -> String -> JSON -> Deserialize //
            var repo = client.GetStringAsync(url).Result;
            var response = JsonSerializer.Deserialize<CharacterDataWrapper>(repo);

            return response;
        }


        // Get List of Characters //
        public CharacterDataWrapper GetListOfCharacters()
        {
            // Random Number for the Offset //
            Random random = new Random();
            var offset = random.Next(_maxCharacters);
            // Assemble The URL //
            string url = $"https://gateway.marvel.com:443/v1/public/characters?&ts=1&limit={_limit}&offset={offset}&apikey={_publicKey}&hash={_md5Hash}";

            // Response -> String -> JSON -> Deserialize //
            var repo = client.GetStringAsync(url).Result;
            var response = JsonSerializer.Deserialize<CharacterDataWrapper>(repo);

            return response;
        }

        // Get Comics For a Character by ID //
        public ComicDataWrapper GetCharacterComics(string characterId)
        {

            // Assemble The URL //
            string url = $"{characterId}?ts=1&limit=20&apikey={_publicKey}&hash={_md5Hash}";

            // Response -> String -> JSON -> Deserialize //
            var repo = client.GetStringAsync(url).Result;
            var response = JsonSerializer.Deserialize<ComicDataWrapper>(repo);

            return response;
        }
    }
}

