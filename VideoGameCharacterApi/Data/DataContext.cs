using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
            //აქ არ გჭირედება options ში რაიმე input ჩაუწერო EF თავისით აგვარებს
        {
                
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        // ამეებით table ები გავაკეთეთ Database-ში

        protected override void OnModelCreating(ModelBuilder modelBuilder) // აქაც ანალოგიურად EF აგვარებს
            //    ეს არის მეთოდი რომლითაც საშუალება გვეძლევა config გავუკეთოთ Tables,
            //    ანუ საშუალება გვეძლევა ვაკონტროლოთ როგორ იქმენაბა Table Database ში
        {
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            // ამ მეთოდით დავაწესეთ რომ PokemonCategory-ს აქვს პრაიმერი გასაღები რომელიც შეიცავს 2 ივე ID-ს.
            // modelbuilder არის instance კლასის
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(p => p.CategoryId);
            // setting up the M2M relationship.
            // HasOne = ყველა პოკემონ კატეგორიას აქვს ერთი კატეგორია.
            // WithMany = 1 კატეგორიას შეიძლება ბევრი პოკემონ კატეგორია ქონდეს.
            // HasForeignKey = უცხოურ გასაღებს ვუთითებთ რაარის.


            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(p => p.PokemonId);

            // ანალოგიურად აქავ იგივე.

        }


    }
}
//overriden მეთოდების input თავისით ივსება.