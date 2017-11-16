using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobUwp.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
