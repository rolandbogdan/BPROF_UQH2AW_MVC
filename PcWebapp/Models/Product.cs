﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public enum ProductCategory
    {
        CPU, Motherboard, RAM, VideoCard, PowerSupply, Storage, Case, Cooler
    }
    public class Product
    {
        [Key]
        public string ProductID { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        public ProductCategory Category { get; set; }
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
