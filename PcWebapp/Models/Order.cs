using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public enum OrderStatus
    {
        pending, awaiting_shipment, shipped, cancelled, disputed
    }

    public class Order
    {
        [Key]
        public string OrderID { get; set; }
        public int OrderedQuantity { get; set; }
        public DateTime OrderDate { get; set; }
        [StringLength(100)]
        public string Comment { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
