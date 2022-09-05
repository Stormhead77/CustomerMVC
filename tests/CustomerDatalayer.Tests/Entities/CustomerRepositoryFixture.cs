using CustomerDatalayer.Entities;
using CustomerDatalayer.Repositories;

namespace CustomerDatalayer.Tests.Entities
{
    public static class CustomersRepositoryFixture
    {
        public static void DeleteAll()
        {
            var repository = new CustomerRepository();
            repository.DeleteAll();
        }

        public static Customer GetCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Harold",
                LastName = "Johnson",
                PhoneNumber = "+12673935933",
                Email = "HaroldSJohnson@armyspy.com",
                TotalPurchasesAmount = 0
            };

            return customer;
        }
    }
}
