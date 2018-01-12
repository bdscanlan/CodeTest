using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace Validus_Code_Test
{
   public abstract class BaseModel
    {

        [Key]
        public long         Id              { get; set; }

        public DateTime     Created         { get; set; }

        public DateTime     LastModified    { get; set; }
    }
}
