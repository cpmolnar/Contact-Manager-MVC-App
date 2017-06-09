using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Globalization;
namespace ContactManager.Models
{
    public class Contact
    {
        private SqlDataReader reader;

        public Contact()
        {

        }
        public Contact(SqlDataReader reader)
        {
            Name = reader.GetString(1);
            Address = reader.GetString(2);
            City = reader.GetString(3);
            State = reader.GetString(4);
            Zip = reader.GetString(5);
            Email = reader.GetString(6);
            OwnerID = reader.GetString(7);

        }

        public int ContactId { get; set; }

        // user ID from AspNetUser table
        public string OwnerID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}