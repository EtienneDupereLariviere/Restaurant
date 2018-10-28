using System.Data.Entity;

namespace Restaurant.Models
{
    public class SqlBdContext : DbContext
    {    
        public SqlBdContext() : base("name=SqlBdContext")
        {
        }

        public System.Data.Entity.DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Resto> Restaurants { get; set; }

        public System.Data.Entity.DbSet<Survey> Surveys { get; set; }
    }
}
