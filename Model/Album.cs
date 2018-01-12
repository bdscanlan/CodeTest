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
    //http://www.entityframeworktutorial.net/code-first/table-dataannotations-attribute-in-code-first.aspx
    [Table("Album")]
    public class Album : BaseModel
    {
        public string name { get; set; }

        public long yearReleased { get; set; }

        public Artist artist { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

    }
}
