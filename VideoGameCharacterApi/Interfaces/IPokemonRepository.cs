using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
    }
}
