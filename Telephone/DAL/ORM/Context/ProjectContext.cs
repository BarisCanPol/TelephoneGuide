using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone.DAL.ORM.Entity;

namespace Telephone.DAL.ORM.Context
{
    public class ProjectContext : DbContext
    {

        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.;Database=Telephone;UID=bcp;PWD=123;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }   
}
