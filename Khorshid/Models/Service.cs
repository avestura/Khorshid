using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Khorshid.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string DriveName { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }

        public string Adreess { get; set; }

        public string Phone { get; set; }


        public string SchoolName { get; set; }

        public string SchoolAdreess { get; set; }

        public string SchoolPhone { get; set; }

        public int PricePay { get; set; }

        public string date { get; set; }
    }
}