using System.Data.Entity;

namespace Restaurant.Models
{
    public class SqlBdContext : DbContext
    {    
        public SqlBdContext() : base("name=SqlBdContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Resto> Restaurants { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Survey>(user => user.Poll)
                .WithMany(servey => servey.Users)
                .Map(us =>
                {
                    us.MapLeftKey("user_id");
                    us.MapRightKey("survey_id");
                    us.ToTable("UserManySurvey");
                });

            modelBuilder.Entity<Resto>()
                .HasMany<Survey>(resto => resto.Poll)
                .WithMany(servey => servey.Restaurants)
                .Map(us =>
                {
                    us.MapLeftKey("resto_id");
                    us.MapRightKey("survey_id");
                    us.ToTable("RestoManySurvey");
                });
        }
    }
}
