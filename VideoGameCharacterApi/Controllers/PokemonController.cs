
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Repository;
namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController:Controller
    {
        public IPokemonRepository _pokemonRepository;


        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }
        
    }
}
