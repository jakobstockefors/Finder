using Data.Models;
using Data.Models.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Data
{
    public class FinderContext : DbContext
    {
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Category> Categories { get; set; }
       

        public FinderContext()
            : base("name=FinderContext")
        {
            // We use a custom database initializer to seed the database and set how it should behave on start
            Database.SetInitializer<FinderContext>(new DropCreateDatabaseIfModelChanges<FinderContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
