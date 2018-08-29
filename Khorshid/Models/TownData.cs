using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Models
{
    public class TownData
    {

        [Key]
        public int Id { get; set; }

        public string Town { get; set; }

        public string Price { get; set; }

        public string Tag { get; set; }

    }
}
