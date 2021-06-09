using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkLab
{
    internal static class Program
    {
        private async static Task<int> Main(String[] args)
        {
            using (Context context = new Context())
            {
                Address address = new Address()
                {
                    City = "Some City",
                    Country = "United Kingdom",
                    County = "Some County",
                    Number = "1076",
                    People = new List<Person>(),
                    Postcode = "Some Postcode",
                    Street = "Some Street"
                };

                Person person = new Person()
                {
                    Address = address,
                    DateOfBirth = DateTime.Now,
                    Forename = "Jane",
                    MiddleName = "Janet",
                    Surname = "Doe"
                };

                context.Addresses.Add(address);
                context.People.Add(person);
                await context.SaveChangesAsync();
            }

            return 0;
        }
    }
}
