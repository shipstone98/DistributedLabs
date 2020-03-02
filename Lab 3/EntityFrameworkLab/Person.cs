using System;

namespace EntityFrameworkLab
{
    public class Person
    {
        public Address Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Forename { get; set; }
        public String MiddleName { get; set; }
        public String Surname { get; set; }

        public Person() { }
    }
}