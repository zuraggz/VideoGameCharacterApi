    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using VideoGameCharacterApi.Dto;
    using VideoGameCharacterApi.Interfaces;
    using VideoGameCharacterApi.Models;
    using VideoGameCharacterApi.Repository;

    namespace VideoGameCharacterApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OwnerController : Controller
        {
            private readonly IOwnerRepository _ownerRepository;
            private readonly IMapper _mapper;
            private readonly IPokemonRepository _pokemonRepository;

            public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, IPokemonRepository pokemonRepository)
            {
                _ownerRepository = ownerRepository;
                _mapper = mapper;
                _pokemonRepository= pokemonRepository;
            }

            [HttpGet("{ownerId}")]
            [ProducesResponseType(200, Type= typeof(OwnerDto))]
            [ProducesResponseType(404)]
            public IActionResult GetOwner(int ownerId)
            {
                if (!_ownerRepository.OwnerExists(ownerId))
                {
                    return NotFound();
                }
                var result = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(result);
            }

            [HttpGet("pokemon/{pokeid}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
            [ProducesResponseType(404)]
        
            public IActionResult GetOwnerOfAPokemon(int pokeId)
            {
            
                if (!_pokemonRepository.PokemonExists(pokeId))
                    return NotFound();

                var result = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnerOfAPokemon(pokeId));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(result);
            }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetOwners()
        {
            var result = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type=typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if(!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var result = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto owner)
        {
            if (owner == null)
            {
                return BadRequest();
            }

            var OwnerCount = _ownerRepository
                .GetOwners().Where(c => c.Name.Trim().ToUpper() == owner.Name.Trim().ToUpper()).FirstOrDefault();

            if (OwnerCount != null)
            {
                ModelState.AddModelError("", "Owner already exsists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapped = _mapper.Map<Owner>(owner);
            if (!_ownerRepository.CreateOwner(mapped))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Owner Created Successfully");

        }
    }
}
