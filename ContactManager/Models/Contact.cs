using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNet.Identity;

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

            string queryString = "SELECT UserName "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE Id = '" + reader.GetString(7) + "'";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            List<Contact> columnData = new List<Contact>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                r.Read();
                OwnerID = r.GetString(0);
                r.Close();
            }

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