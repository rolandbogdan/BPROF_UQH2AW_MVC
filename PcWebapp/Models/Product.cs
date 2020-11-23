using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Product
    {
        [Key]
        public string ProductID { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(50)]
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        [StringLength(500)]
        public string Description { get; set; }


        [NotMapped]
        public virtual Customer Customer { get; set; }
        public string CustomerID { get; set; }

    }
}
