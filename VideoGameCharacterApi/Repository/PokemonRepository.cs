using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context= context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p=>p.Name==name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var Reviews = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if (Reviews.Count() <= 0)
            {
                return 0;
            }

            return (decimal)Reviews.Sum(r=>r.Rating)/Reviews.Count();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p=>p.Id == pokeId);
        }
    }
}
