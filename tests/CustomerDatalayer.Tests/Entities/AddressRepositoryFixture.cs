using CustomerDatalayer.Entities;
using CustomerDatalayer.Repositories;

namespace CustomerDatalayer.Tests.Entities
{
    public static class AddressesRepositoryFixture
    {
        public static void DeleteAll()
        {
            var repository = new AddressRepository();
            repository.DeleteAll();
        }

        public static Address GetAddress()
        {
            CustomersRepositoryFixture.DeleteAll();
            var repository = new CustomerRepository();
            var customer = CustomersRepositoryFixture.GetCustomer();
            var createdCustomer = repository.Create(customer);

            var address = new Address
            {
                CustomerId = createdCustomer.Id,
                AddressLine = "4100 Holly Street",
                AddressLine2 = "4101 Holly Street",
                Type = "Billing",
                City = "Blue Ridge",
                PostalCode = "30513",
                State = "GA",
                Country = "United States"
            };

            return address;
        }
    }
}
