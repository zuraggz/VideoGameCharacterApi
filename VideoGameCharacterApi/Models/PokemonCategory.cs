namespace VideoGameCharacterApi.Models
{
    public class PokemonCategory
    {
        public int PokemonId { get; set; }
        public int CategoryId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Category Category { get; set; }

    }
}

// ეს არის უბრალო Data რომელშიც ვა define ებთ რა ინფორმაციას უნდა შეიცავდეს Table.
