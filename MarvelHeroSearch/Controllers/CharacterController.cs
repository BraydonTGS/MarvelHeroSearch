using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelHeroSearch.Client;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarvelHeroSearch.Controllers
{
    public class CharacterController : Controller
    {

        private readonly IMarvelApiClient _response;

        public CharacterController(IMarvelApiClient response)
        {
            _response = response;
        }

        // Home Page Character Controller //
        public IActionResult Index()
        {
            return View();
        }

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

            //var myComics = _response.GetCharacterComics(character.comics.collectionURI);

            // Character Name Length Shortened to Fit the Card //
            if (character.name.Length > 15)
            {
                character.name = character.name.Substring(0, 15) + "...";
            }
            if (character.description.Length > 360)
            {
                character.description = character.description.Substring(0, 360) + "...";
            }
            if (character.description == "")
            {
                character.description = "IF YOU’RE NOTHING WITHOUT THIS SUIT, THEN YOU SHOULDN’T HAVE IT.";
            }


            return View(root);
        }

        // Get a List of 100 Random Characters //
        public IActionResult CharacterList()
        {
            var root = _response.GetListOfCharacters();
            var characters = root.data.results;

            // List For the Parsed Results //
            List<Models.Hero.Character> parsedCharacters = new List<Models.Hero.Character>();

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

        public IActionResult CharacterNotFound()
        {
            return View();
        }
    }
}

