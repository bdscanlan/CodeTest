using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;


namespace Validus_Code_Test
{
    [Table("Song")]
    public class Song : BaseModel
    {
        public string       name    { get; set; }

        [Column("album_id")]
        public Album        album   { get; set; }

        public int?         track    { get; set; }
    }
}
