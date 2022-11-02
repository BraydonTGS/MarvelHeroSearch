﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelHeroSearch.Client;
using MarvelHeroSearch.Models.DbModels;
using MarvelHeroSearch.Models.Hero;
using MarvelHeroSearch.Models.HeroComics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarvelHeroSearch.Controllers
{
    public class CharacterController : Controller
    {

        private readonly IMarvelApiClient _response;

        private readonly IHeroRepository _heroResponse;


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
            var searchString = Request.Form["searchString"];
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return View("CharacterNotFound");
            }
            var root = _response.GetCharacter(searchString);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }

            var character = root.data.results[0];

            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

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

            return View(root);
        }

        // Search for a Character by Id //
        public IActionResult CharacterSearchById()
        {
            var characterId = Request.Form["searchString"];
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }
            var root = _response.GetCharacterById(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            var character = root.data.results[0];

            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

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

            return View(root);
        }

        // Get a List of 100 Random Characters //
        public IActionResult CharacterList()
        {
            var root = _response.GetListOfCharacters();
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

            return View(parsedCharacters);
        }

        // Get List of Favorite Characters //
        public IActionResult Favorites()
        {
            List<Character> favoriteCharacters = new List<Character>();
            var favorites = _heroResponse.GetAllHeroes();
            if (!favorites.Any())
            {
                return View("CharacterFavoritesEmpty");
            }
            // Calling the API for each Character based on Character Id //
            foreach (var hero in favorites)
            {
                var character = _response.GetCharacterById(hero.CharacterId);
                var favoriteCharacter = character.data.results[0];
                favoriteCharacters.Add(favoriteCharacter);
            }

            return View(favoriteCharacters);
        }

        // Add to your Favorite Characters //
        public IActionResult AddToFavorites()
        {
            var characterId = Request.Form["searchString"];

            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }

            var root = _response.GetCharacterById(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            var character = root.data.results[0];

            var favCharacter = new CharacterDb();
            favCharacter.CharacterName = character.name;
            favCharacter.CharacterId = character.id.ToString();

            _heroResponse.InsertHero(favCharacter);
            return RedirectToAction("Favorites");
        }

        // Delete Character //
        public IActionResult DeleteHero(Character character)
        {
            _heroResponse.DeleteHero(character);
            return RedirectToAction("Favorites");

        }

        // Get a List of Comics for Each Character by Character Name //
        public IActionResult GetComicsByHeroName()
        {
            var characterId = Request.Form["searchString"];

            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }
            var root = _response.GetCharacter(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            var character = root.data.results[0];

            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

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
            return View(root);
        }

        // Get a List of Comics for Each Character by Character ID //
        public IActionResult GetComicsbyHeroId()
        {
            var characterId = Request.Form["searchString"];
            if (string.IsNullOrWhiteSpace(characterId))
            {
                return View("CharacterNotFound");
            }
            var root = _response.GetCharacterById(characterId);

            if (!root.data.results.Any())
            {
                return View("CharacterNotFound");

            }
            var character = root.data.results[0];

            var myComics = _response.GetCharacterComics(character.comics.collectionURI);

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

