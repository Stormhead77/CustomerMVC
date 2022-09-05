using CustomerDatalayer.Entities;

namespace CustomerDatalayer.Tests.Entities
{
    public class CustomerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Harold",
                LastName = "Johnson",
                PhoneNumber = "+12673935933",
                Email = "HaroldSJohnson@armyspy.com",
                TotalPurchasesAmount = 0
            };

            Assert.NotNull(customer);
            Assert.Equal("Harold", customer.FirstName);
            Assert.Equal("Johnson", customer.LastName);
            Assert.Equal("+12673935933", customer.PhoneNumber);
            Assert.Equal("HaroldSJohnson@armyspy.com", customer.Email);
            Assert.Equal(0m, customer.TotalPurchasesAmount);
        }
    }
}
