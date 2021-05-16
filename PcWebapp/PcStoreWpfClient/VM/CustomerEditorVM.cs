using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcStoreWpfClient.VM
{
    class CustomerEditorVM : ViewModelBase
    {
        private Customer customer;

        public CustomerEditorVM()
        {
            this.customer = new Customer();
            if (this.IsInDesignMode)
            {
                this.Customer.CustomerName = "Bob Testing";
                this.Customer.EmailAddress = "bob@gmail.com";
                this.Customer.Password = "Bobvagyok123";
                this.Customer.Address = "Budapest Fő utca 1.";
                this.Customer.PhoneNumber = "+36 30 300 3000";
            }
        }

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
    }
}
