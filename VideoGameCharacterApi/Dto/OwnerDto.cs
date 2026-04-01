using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Dto
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gym { get; set; }
        public Country Country { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
