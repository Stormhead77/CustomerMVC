using CustomerDatalayer.Entities;

namespace CustomerDatalayer.Tests.Entities
{
    public class AddressTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var address = new Address
            {
                AddressLine = "4100 Holly Street",
                AddressLine2 = "4101 Holly Street",
                Type = "Billing",
                City = "Blue Ridge",
                PostalCode = "30513",
                State = "GA",
                Country = "United States"
            };

            Assert.NotNull(address);
            Assert.Equal("4100 Holly Street", address.AddressLine);
            Assert.Equal("4101 Holly Street", address.AddressLine2);
            Assert.Equal("Billing", address.Type);
            Assert.Equal("Blue Ridge", address.City);
            Assert.Equal("30513", address.PostalCode);
            Assert.Equal("GA", address.State);
            Assert.Equal("United States", address.Country);
        }
    }
}
