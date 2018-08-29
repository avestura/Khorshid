using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Models
{
    public class DriverWork
    {
        [Key]
        public int Id { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public int Price { get; set; }

        public DateTime Date { get; set; }

        public int? WorkPageId { get; set; }

        [ForeignKey(nameof(WorkPageId))]
        public WorkPage WorkPage { get; set; }

    }
}
