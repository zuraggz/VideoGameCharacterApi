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
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IMapper mapper,IReviewRepository reviewRepository)
        {
            _mapper=mapper;
            _reviewRepository=reviewRepository;
        }

        

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
            if (!_reviewRepository.ReviewExists(pokeId))
            {
                return NotFound();
            }
            var result = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(pokeId));
            
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(result);

        }


        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var result= _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDto review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            var ReviewCount = _reviewRepository
                .GetReviews().Where(c => c.Title.Trim().ToUpper() == review.Title.Trim().ToUpper()).FirstOrDefault();

            if (ReviewCount != null)
            {
                ModelState.AddModelError("", "Review already exsists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapped = _mapper.Map<Review>(review);
            if (!_reviewRepository.CreateReview(mapped))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Review Created Successfully");

        }
    }
}
