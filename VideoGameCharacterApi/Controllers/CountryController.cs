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
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _mapper=mapper;
            _countryRepository=countryRepository;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExsists(countryId))
            {
                return NotFound();
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);


        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var countries = _mapper.Map<CountryDto>
                (_countryRepository.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countries);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody]CountryDto country)
        {
            if(country == null)
            {
                return BadRequest();
            }

            var CountryCount= _countryRepository
                .GetCountries().Where(c=>c.Name.Trim().ToUpper()==country.Name.Trim().ToUpper()).FirstOrDefault();

            if(CountryCount != null)
            {
                ModelState.AddModelError("", "Country already exsists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapped = _mapper.Map<Country>(country);
            if (!_countryRepository.CreateCountry(mapped))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Country Created Successfully");

        }




    }
}
