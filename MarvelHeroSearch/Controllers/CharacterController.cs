using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.DbModels;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Models.HeroComics;
using Microsoft.AspNetCore.Mvc;

namespace MarvelHeroSearch.Controllers
{
    public class CharacterController : Controller
    {
        // Web API //
        private readonly IMarvelApiClient _response;

        // MySQL DB //
        private readonly IHeroRepository _heroResponse;

        // Dependecny Injection //
        public CharacterController(IMarvelApiClient response, IHeroRepository hResponse)
        {
            _response = response;
            _heroResponse = hResponse;
        }


        // Home Page Character Controller //
        public IActionResult Index()
        {
            return View();
        }

        // Searching for a Character by Name //
        public IActionResult CharacterSearch()
        {
            // Grabbing the Name from the Form //
            var searchString = Request.Form["searchString"];
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return View("CharacterNotFound");
            }

            // Character Data Wrapper => Represents everything that a Character has //
            var root = _response.GetCharacter(searchString);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            // Character Model //
            var character = root.data.results[0];

            // Getting the Comics for a Character //
            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

            // Parsing Through the Comics for a Character and adding to the Characters List of Comics //
            foreach (var comic in myComics.data.results)
            {
                if (comic.thumbnail != null && comic.thumbnail.IsImageFound)
                {
                    character.ComicBooks.Add(comic);
                }
            }

            if (character.description == "")
            {
                character.description = "If You're Nothing Without The Suit, Then You Shouldn't Have It.";
            }

            // Sending the Character to the View //
            return View(root);
        }

        // Search for a Character by Id //
        public IActionResult CharacterSearchById()
        {
            // Grabbing the ID from the Form //
            var characterId = Request.Form["searchString"];
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }

            // Character Data Wrapper => Represents everything that a Character has //
            var root = _response.GetCharacterById(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            // Character Model //
            var character = root.data.results[0];

            // Getting the Comics for a Character //
            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

            // Parsing Through the Comics for a Character and adding to the Characters List of Comics //
            foreach (var comic in myComics.data.results)
            {
                if (comic.thumbnail != null && comic.thumbnail.IsImageFound)
                {
                    character.ComicBooks.Add(comic);
                }
            }

            if (string.IsNullOrWhiteSpace(character.description))
            {
                character.description = "If You're Nothing Without The Suit, Then You Shouldn't Have It.";
            }

            // Sending Character to the View //
            return View(root);
        }

        // Get a List of 100 Random Characters //
        public IActionResult CharacterList()
        {
            // Character Data Wrapper //
            var root = _response.GetListOfCharacters();

            // List of Characters //
            var characters = root.data.results;

            // List For the Parsed Results //
            List<Character> parsedCharacters = new List<Character>();

            foreach (var character in characters)
            {
                // Filer Characters Results Missing Thumbnail Image // 
                if (character.thumbnail != null && character.thumbnail.IsImageFound)
                {
                    // Character Name Length Shortened to Fit the Card //
                    if (character.name.Length > 15)
                    {
                        character.name = character.name.Substring(0, 15) + "...";
                    }
                    parsedCharacters.Add(character);
                }
            }

            // Sending Parsed List to the View //
            return View(parsedCharacters);
        }

        // Get List of Favorite Characters //
        public IActionResult Favorites()
        {
            // Creating an Empty List of Fav Characters //
            List<Character> favoriteCharacters = new List<Character>();

            // Getting all my Characters from the DB //
            var favorites = _heroResponse.GetAllHeroes();

            // Checking if Any... if not return View //
            if (!favorites.Any())
            {
                return View("CharacterFavoritesEmpty");
            }

            foreach (var hero in favorites)
            {
                // Calling the API for each Character based on Character Id //
                var character = _response.GetCharacterById(hero.CharacterId);

                // Grabbing the Character from the Returned Results //
                var favoriteCharacter = character.data.results[0];

                // Adding the Character to the List of Favorites //
                favoriteCharacters.Add(favoriteCharacter);
            }

            // Sending the List to the view //
            return View(favoriteCharacters);
        }

        // Add to your Favorite Characters //
        public IActionResult AddToFavorites()
        {
            // Grabbing the Character ID //
            var characterId = Request.Form["searchString"];

            // Empty Check //
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }

            // Character Data Wrapper //
            var root = _response.GetCharacterById(characterId);

            // Null Check //
            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }

            // Represents a Character from the Returned Results //
            var character = root.data.results[0];

            // Creating an Instance of CharacterDb //
            var favCharacter = new CharacterDb();

            // Setting the Values //
            favCharacter.CharacterName = character.name;
            favCharacter.CharacterId = character.id.ToString();

            // Inserting Character to DB //
            _heroResponse.InsertHero(favCharacter);

            // Redirect to Favorites View //
            return RedirectToAction("Favorites");
        }

        // Delete Character //
        public IActionResult DeleteHero(Character character)
        {
            // Delete Character from DB //
            _heroResponse.DeleteHero(character);

            // Redirect to Favorites //
            return RedirectToAction("Favorites");

        }

        // Get a List of Comics for Each Character by Character Name //
        public IActionResult GetComicsByHeroName()
        {
            // Character Name //
            var characterId = Request.Form["searchString"];

            // Null Check //
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }
            // Character Data Wrapper //
            var root = _response.GetCharacter(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            // Character from Results //
            var character = root.data.results[0];

            // Grabbing the Comics for This Character //
            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

            // Parsing Comic Information //
            foreach (var comic in myComics.data.results)
            {
                if (comic.thumbnail != null && comic.thumbnail.IsImageFound)
                {
                    if (string.IsNullOrWhiteSpace(comic.description))
                    {
                        comic.description = "No Detailed Comic Information Found.";
                    }
                    character.ComicBooks.Add(comic);
                }
                if (comic.title.Length > 25)
                {
                    comic.title = comic.title.Substring(0, 25) + "...";
                }

            }
            // Sending Character + Comics to View //
            return View(root);
        }

        // Get a List of Comics for Each Character by Character ID //
        public IActionResult GetComicsbyHeroId()
        {
            // Character Id //
            var characterId = Request.Form["searchString"];

            // Null Check //
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }

            // Character Data Wrapper //
            var root = _response.GetCharacterById(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }

            // Character from Results //
            var character = root.data.results[0];

            // Get Comics by Id //
            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

            // Parsing Comics //
            foreach (var comic in myComics.data.results)
            {
                if (comic.thumbnail != null && comic.thumbnail.IsImageFound)
                {
                    if (string.IsNullOrWhiteSpace(comic.description))
                    {
                        comic.description = "No Detailed Comic Information Found";
                    }
                    character.ComicBooks.Add(comic);
                }
                if (comic.title.Length > 25)
                {
                    comic.title = comic.title.Substring(0, 25) + "...";
                }

            }

            // Sending Character + Comics to the View //
            return View(root);
        }

        // Comic Home Page //
        public IActionResult GetComics()
        {
            return View();
        }

        // Character Not Found //
        public IActionResult CharacterNotFound()
        {
            return View();
        }


    }
}

