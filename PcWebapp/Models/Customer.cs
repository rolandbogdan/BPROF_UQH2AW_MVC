using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerID { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        public DateTime RegDate { get; set; }
        [StringLength(32)]
        public string Password { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Order Order { get; set; }
        public string OrderID { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
