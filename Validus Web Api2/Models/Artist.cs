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
    [Table("Artist")]
    public class Artist : BaseModel
    {
        public string name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
