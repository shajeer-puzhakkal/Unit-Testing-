using CloudCustomers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTest.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new List<User>
        { 
            new User()
            {
                Id = 1,
                Name ="Test User 1",
                Address= new Address()
                {
                    City ="MBZ1",
                    Street="Z1",
                    ZipCode="151"

                },
                Email ="test1@gmail.com"
            },
            new User()
            {
                Id = 2,
                Name ="Test User 2",
                Address= new Address()
                {
                    City ="MBZ2",
                    Street="Z2",
                    ZipCode="152"

                },
                Email ="test2@gmail.com"
            },
            new User()
            {
                Id = 3,
                Name ="Test User 3",
                Address= new Address()
                {
                    City ="MBZ3",
                    Street="Z3",
                    ZipCode="153"

                },
                Email ="test3@gmail.com"
            }
        };

    }
}
