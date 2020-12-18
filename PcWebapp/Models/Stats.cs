using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Models
{
    class Stats
    {
        //100 ezernél drágább rendelések
        public IEnumerable<Order> ExpensiveOrders { get; set; }

        //legrégebbi felhasználó rendelései
        public IEnumerable<Order> LongestUserOrders { get; set; }

        //xy gyártó termékeinek rendelői
        public IEnumerable<Customer> CustomersOfManufacturer { get; set; }



    }
}
