using PorwalGeneralStore.EdmxModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryDbContext
{
    public static class CustomerInfoData
    {
        public static List<CustomerInfo> GetCustomerInformation()
        {
            return new List<CustomerInfo>()
            {
                new CustomerInfo()
                {
                    Id=1,
                    City="Indore",
                    Phone="123456",
                    CustomerName="Kuldeep",
                    FirstName="Kuldeep Porwal",
                    LastName="Porwal",
                    Password="12345"
                },
                new CustomerInfo()
                {
                    Id=2,
                    City="Indore",
                    Phone="123456",
                    CustomerName="Vinod Sahu",
                    FirstName="Vinod",
                    LastName="Sahu",
                    Password="789456123"
                },
                new CustomerInfo()
                {
                    Id=3,
                    City="Indore",
                    Phone="123456",
                    CustomerName="Aman pal",
                    FirstName="Aman",
                    LastName="Pal",
                    Password="Pal123"
                }
            };
        }
    }
}
