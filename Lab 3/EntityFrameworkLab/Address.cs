using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkLab
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        
        public String City { get; set; }
        public String Country { get; set; }
        public String County { get; set; }
        public String Number { get; set; }
        public ICollection<Person> People { get; set; }
        public String Postcode { get; set; }
        public String Street { get; set; }

        public Address() { }
    }
}