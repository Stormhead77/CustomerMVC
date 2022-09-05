using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CustomerDatalayer.Entities
{
    public class Address
    {
        public int Id { get; set; } = -1;
        public int CustomerId { get; set; } = -1;
        [Required]
        [MaxLength(100)]
        public string AddressLine { get; set; } = string.Empty;
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        public string Type { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(6)]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Address() { }
        public Address(SqlDataReader reader)
        {
            Id = (int)reader["AddressID"];
            CustomerId = (int)reader["CustomerId"];
            AddressLine = (string)reader["AddressLine"];
            AddressLine2 = (string)reader["AddressLine2"];
            Type = (string)reader["AddressType"];
            City = (string)reader["City"];
            PostalCode = (string)reader["PostalCode"];
            State = (string)reader["State"];
            Country = (string)reader["Country"];
        }
    }
}
