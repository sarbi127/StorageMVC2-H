using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorageMVC2.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [Required]
        public string Name { get; set; }

        [Range(0, 4000000)]
        public int Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Date of order")]
        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }

        public string Category { get; set; }

        public string Shelf { get; set; }

        [Range(0, 10)]
        public int Count { get; set; }

        public string Description { get; set; }



    }
}
