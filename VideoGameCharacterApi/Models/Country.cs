using System.Security.Principal;

namespace VideoGameCharacterApi.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Owner> Owenrs { get; set; }
    }
}
