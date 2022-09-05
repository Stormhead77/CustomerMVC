using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CustomerDatalayer.Entities
{
    public class Customer
    {
        public int Id { get; set; } = -1;
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public decimal? TotalPurchasesAmount { get; set; } = 0;
 
        public Customer() { }
        public Customer(SqlDataReader reader)
        {
            Id = (int)reader["CustomerId"];
            FirstName = reader["FirstName"] == DBNull.Value
                ? null
                : (string)reader["FirstName"];
            LastName = (string)reader["LastName"];
            PhoneNumber = reader["PhoneNumber"] == DBNull.Value
                ? null
                : (string)reader["PhoneNumber"];
            Email = reader["Email"] == DBNull.Value
                ? null
                : (string)reader["Email"];
            TotalPurchasesAmount = reader["TotalPurchasesAmount"] == DBNull.Value
                ? null
                : (decimal?)reader["TotalPurchasesAmount"];
        }
    }
}