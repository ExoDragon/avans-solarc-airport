using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEventHandler.Domain
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }

        public Customer() { }

        public Customer(string id, string name, string email, string phone, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phonenumber = phone;
            Address = address;
        }
    }
}
