using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Models
{
    public class WorkPage
    {
        [Key]
        public int Id { get; set; }

        public byte CommissionPercentage { get; set; }

        public bool IsClosed { get; set; }

        public DateTime? DateClosed { get; set; }

        public int DriverId { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

    }
}
