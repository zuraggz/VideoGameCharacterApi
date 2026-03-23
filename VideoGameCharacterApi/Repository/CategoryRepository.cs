using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Interfaces;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c=>c.Id).ToList();
        }

        public Category GetCategory(int id)
        {

            return _context.Categories.Where(c =>c.Id==id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(pc=>pc.CategoryId==categoryId).Select(p=>p.Pokemon).ToList();
            //გადავდივართ პოკემონკატეგორიაში და მოგვაგ ის პოკემონი
        }
    }
}
