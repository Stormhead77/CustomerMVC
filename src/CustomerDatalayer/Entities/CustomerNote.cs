using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CustomerDatalayer.Entities
{
    public class CustomerNote
    {
        public int CustomerId { get; set; }
        [Required]
        public string NoteText { get; set; } = string.Empty;

        public CustomerNote() { }
        public CustomerNote(SqlDataReader reader)
        {
            CustomerId = (int)reader["CustomerId"];
            NoteText = (string)reader["NoteText"];
        }
    }
}
