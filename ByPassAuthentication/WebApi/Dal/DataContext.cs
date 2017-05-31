using System.Data.Entity;
using WebApi.Models;

namespace WebApi.Dal
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        public DataContext() : base("Connection")
        { }
        public virtual DbSet<Model> Models => Set<Model>();
    }
}