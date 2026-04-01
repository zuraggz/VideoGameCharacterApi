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
    public class ReviewerController :Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository=reviewerRepository;
            _mapper=mapper;
            
        }


        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type=typeof(ReviewerDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var result = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

            

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(ICollection<ReviewerDto>))]
        public IActionResult GetReviewers()
        {
            var result = _mapper.Map<ICollection<ReviewerDto>>(_reviewerRepository.GetReviewers());
            
            return Ok(result);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type= typeof(ICollection<ReviewDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var result = _mapper.Map<ICollection<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
            
            return Ok(result);
        }

        
    }
}
