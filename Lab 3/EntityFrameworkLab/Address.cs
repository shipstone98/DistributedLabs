using System;
using System.Collections.Generic;

namespace EntityFrameworkLab
{
    public class Address
    {
        public String City { get; set; }
        public String County { get; set; }
        public String Number { get; set; }
        public ICollection<Person> People { get; set; }
        public String Postcode { get; set; }
        public String Street { get; set; }

        public Address() { }
    }
}