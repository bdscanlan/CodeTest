using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Validus_Code_Test
{

    public class MusicContext : DbContext
    {
        public DbSet<Song> Song { get; set; }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<Album> Album { get; set; }

        //public DbSet<Artist_Album> Album_Artists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //http://www.c-sharpcorner.com/UploadFile/a3d5d0/entity-framework-code-first-approach-plural-table-name/
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           

            //modelBuilder.Entity<Album>().Property(p => p.Id)
            //   .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            //modelBuilder.Entity<Song>().Property(p => p.Id)
            // .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            //modelBuilder.Entity<Artist>().Property(p => p.Id)
            // .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
        }

       

    }
}
